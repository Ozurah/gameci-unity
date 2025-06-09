# Activer l'étape de build

Configuration du workflow github :
1) Sur github --> `Settings > Secrets and variables > Actions > Variables > Repository variables`
2) Ajouter une variable `ENABLE_BUILD`
3) mettre (exclusivement, case sensitive) ``true`` pour **activé**; **autrement** c'est **désactivé**

# Type de build

Liste des builds supportés :
> https://game.ci/docs/github/builder/#complete-example
- StandaloneOSX # Build a macOS standalone (Intel 64-bit).
- StandaloneWindows # Build a Windows standalone.
- StandaloneWindows64 # Build a Windows 64-bit standalone.
- StandaloneLinux64 # Build a Linux 64-bit standalone.
- iOS # Build an iOS player.
- Android # Build an Android .apk standalone app.
- WebGL # WebGL.

Configuration du workflow github :
1) Sur github --> `Settings > Secrets and variables > Actions > Variables > Repository variables`
2) Ajouter une variable `TARGET_PLATFORMS_JSON`
3) Lui définire le json avec les builds souhaités
    - Exemple
        ```json
        [
        "StandaloneWindows64",
        "Android",
        "WebGL"
        ]
        ```

La matrice des build a effectuer est calculée au démarrage du workflow !
