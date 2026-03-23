# Epsi.MazeCs

Jeu de labyrinthe ASCII en C# (console) : vous déplacez `@` dans un labyrinthe généré procéduralement pour atteindre la sortie `★`.

Le labyrinthe contient :
- des murs (`Wall`),
- des salles (`Room`) pouvant contenir des objets,
- des portes (`Door`) verrouillées,
- des clés (`Key`) persistantes dans l'inventaire,
- des pièces (`Coin`) qui rapportent des points.

## Prérequis

- SDK .NET `9.0` (projet cible `net9.0`)
- Terminal compatible Unicode (Windows Terminal recommandé)

Vérifier la version installée :

```bash
dotnet --version
```

## Lancer le projet

Depuis la racine du repo :

```bash
dotnet run --project Program.csproj
```

## Contrôles

- `Z` ou `↑` : monter
- `S` ou `↓` : descendre
- `Q` ou `←` : gauche
- `D` ou `→` : droite
- `E` ou `Espace` : ramasser l'objet de la case actuelle
- `Échap` : quitter

## Règles de jeu

- Les portes sont générées avec une probabilité configurable (`doorSpawnProbability`).
- Chaque porte instancie sa propre clé à la création.
- Les clés sont placées par `MazeGen` sur le chemin du joueur (retour de récursion) avant la porte correspondante.
- Le joueur ne peut franchir une cellule que via le polymorphisme de `Cell` (`CanBeEnteredBy`) ; `Player` ne connaît pas les types concrets (`Door`, `Wall`, etc.).

## Architecture rapide

- `Program.cs` : composition racine et boucle de jeu.
- `MazeGen` : génération du labyrinthe (DFS), placement des portes et des clés.
- `Maze` : stockage de la grille `Cell[,]`, rendu et accès aux cellules.
- `Player` : déplacements, ramassage, score et inventaire.
- `Cell` : point d'extension polymorphe des comportements de case.

## Notes

- `StartRoom` et `ExitRoom` héritent de `Room` pour marquer le départ et la sortie.
- Le score est lié aux collectables non persistants (ex: `Coin`), tandis que les clés restent dans l'inventaire.
