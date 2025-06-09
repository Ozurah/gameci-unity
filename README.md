# github-gameci-unity

https://game.ci/docs/github/getting-started

# Timeline

## Création de l'action

`.github/workflows/main`

Basé sur :

GameCI Github v4 SimpleExample
(https://game.ci/docs/github/getting-started#simple-example)

puis sur `complete example` (https://game.ci/docs/github/builder/#complete-example)

avec qq ajustements 😊

Prévu pour le projet Unity dans un sous dossier du repo

## Configuration secrets

Ajout des 3 secrets

## 1ere action 
après avoir créé le fichier `.github/workflows/main` --> echec de l'action

Causée par `actions/upload-artifact@v3` qui est dépreciée

Solution -> remplacer par `actions/upload-artifact@v4`

## 2eme tentative de l'action

Echec :

Job "Checkout repository" 
> Fetching LFS objects
  /usr/bin/git lfs fetch origin refs/remotes/origin/main
  fetch: Fetching reference refs/remotes/origin/main
  [1d17a9ff3537859abe2c008603d29d90cf43191ae77aa386167cc65e53ba03d9] Object does not exist on the server: [404] Object does not exist on the server
  Error: error: failed to fetch some objects from 'https://github.com/Ozurah/gameci-unity.git/info/lfs'

Solution -> https://stackoverflow.com/questions/52612880/lfs-upload-missing-object-but-the-file-is-there
- (non sur si ça a été utile, mais j'ai fait aussi cette commande, sur le fichier qui était caché par cet id)
    -  `git add --renormalize .\GameCI-Unity\Assets\TutorialInfo\Icons\URP.png`
-  `git lfs push origin --all`

## 3eme tentative

Echec job "Run tests"

Error: Project settings file not found at "ProjectSettings/ProjectVersion.txt". Have you correctly set the projectPath?

Solution -> Ajout du `project path` (chemin du projet Unity, par relatif au repo) pour le "step" `Tests`

- ```yaml
     # Test
      - name: Run tests
        uses: game-ci/unity-test-runner@v4
        env:
          UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
          UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
          UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
        with:
          githubToken: ${{ secrets.GITHUB_TOKEN }}
          projectPath: 'GameCI-Unity'
    ```

Ajout supplémentaire : idem pour le "step" `Build` (requis)

## 4eme tenta

A la fin du step `Tests` --> Error: Resource not accessible by integration`

Solution --> 
- https://github.com/Ozurah/gameci-unity/settings/actions
- Workflow permissions
- Activation des permissions en RW (au lieu de RO) (pour chaque scope)


Remarque 
- Peut être serait-il interessant de configurer les permissions que pour les scopes nécessaires ?

- Exemple
    - ```yaml
        runs-on: ubuntu-latest
            permissions:
            contents: write
            pull-requests: write
            repository-projects: write
            ```

## Dirty branch

Exécuter les tests PUIS le build peut avoir un problème de branche "dirty" au moment du build.

(si j'exécute que les build, sans les tests, je n'ai pas eu ce problème)

```
Run game-ci/unity-builder@v4
Warning: Changes were made to the following files and folders:

Warning:  M GameCI-Unity/Packages/manifest.json
 M GameCI-Unity/Packages/packages-lock.json
?? GameCI-Unity/ProjectSettings/SceneTemplateSettings.json
?? artifacts/

Error: Branch is dirty. Refusing to base semantic version on uncommitted changes
```

### `GameCI-Unity/ProjectSettings/SceneTemplateSettings.json`

Solutionnée en ajoutant les tests automatisée au projet, puis en les exécutant une première fois les tests (playmode) en local (push du fichier qui a été généré en plus des tests)

### `GameCI-Unity/Packages/manifest.json` et `GameCI-Unity/Packages/packages-lock.json`

En ajoutant un affichage des modifs de ces fichiers dans les étapes du job

```yaml
      - name: Cat Info
        run: |
          cat 'GameCI-Unity/Packages/manifest.json'
          cat 'GameCI-Unity/Packages/packages-lock.json'
```

un package est ajouté via la CI (qui fonctionne sous linux)


`"com.unity.toolchain.linux-x86_64": "2.0.10"`

qui inclue les dépendances :

```json
    "com.unity.sysroot": {
      "version": "2.0.10",
      "depth": 1,
      "source": "registry",
      "dependencies": {},
      "url": "https://packages.unity.com"
    },
    "com.unity.sysroot.linux-x86_64": {
      "version": "2.0.9",
      "depth": 1,
      "source": "registry",
      "dependencies": {
        "com.unity.sysroot": "2.0.10"
      },
      "url": "https://packages.unity.com"
    },
    "com.unity.toolchain.linux-x86_64": {
      "version": "2.0.10",
      "depth": 0,
      "source": "registry",
      "dependencies": {
        "com.unity.sysroot": "2.0.10",
        "com.unity.sysroot.linux-x86_64": "2.0.9"
      },
      "url": "https://packages.unity.com"
    },
```

solution --> ajouter le package "toolchain.linx" au projet

Remarque : en mettant en parallèle les étapes de tests/build, il est possiblement non nécessaire de rajouter la dépendance (comme celà utiliserais plusieurs dockers au lieu d'un seul)

https://github.com/game-ci/unity-builder/issues/554

### `artifacts/`

la CI créé un dossier `artifacts` à la racine du repo

solution --> Simplement le gitignore :)

(je l'avais ignore dans le projet unity, et pas à la racine...)


## Pas assé de place

```
docker: failed to register layer: write /opt/unity/Editor/Data/PlaybackEngines/WebGLSupport/BuildTools/Emscripten/emscripten/cache/sysroot/lib/wasm32-emscripten/pic/libwasmfs-mt-debug.a: no space left on device

Run 'docker run --help' for more information
Error: Build failed with exit code 125
```

--> https://game.ci/docs/troubleshooting/common-issues/#no-space-left-on-device

Solution : soit nettoyer (si il manque peu de place), soit faire en parallèle

--> j'ai opté pour le parallèle (2 jobs : `Test my project 🧪` et `Build my project ✨`)

## Configuration du workflow via GitHub

[Doc de configuration](<.github/workflows/_Info configuration.md>)

## Build que si je l'active

Problème : version gratuite de github = 2000 min d'action / mois
build (du moins que pour webgl) = ~50 min

J'ai donc mis une variable d'activation du job de build :

- Via la variable `ENABLE_BUILD` dans `Settings > Secrets and variables > Actions > Variables > Repository variables`
- Le job de build n'est activé que si la variable vaut (**strictement**) `true`

## Type de build

Sélectionner les plateforme de builds via
- la variable `TARGET_PLATFORMS_JSON` dans `Settings > Secrets and variables > Actions > Variables > Repository variables`

