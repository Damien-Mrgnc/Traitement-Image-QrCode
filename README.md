
# Projet Problème Informatique

Voici le ReadMe du projet [QrCode](<https://cdn.discordapp.com/attachments/1053021149166047284/1229693313414856745/QRCode.bmp?ex=66309c24&is=661e2724&hm=98798254c2e11b0f6a792f76842748c702049044287370566430c94d7dc2b635&>) C# fait par Morganico Damien et Dupin Mattéon


## Qu'est ce que c'est C# ?
-[C#](<https://fr.wikipedia.org/wiki/C_sharp#:~:text=C%23%20est%20un%20langage%20de,ou%20des%20biblioth%C3%A8ques%20de%20classes.(programming_language)&action=edit&redlink=1>) est un langage de programmation orientée objet, fortement typé, dérivé de C et de C++, ressemblant au langage Java3. Il est utilisé pour développer des applications web, ainsi que des applications de bureau, des services web, des commandes, des widgets ou des bibliothèques de classes3. En C#, une application est un lot de classes où une des classes comporte une méthode Main, comme cela se fait en Java3.
## Installation
Pour ce Projet, l'application VisualStudio à été utilisé
- Téléchargez le fichier d'installation « [VisualStudio.exe](https://visualstudio.microsoft.com/fr/downloads/) » (version 2022) sur le site : 

- Double-cliquez sur "Téléchargez Gratuit" dans l'onglet "Community".

- Suivez les instructions d'installations à l'écran pour procéder à l'installation de Visual VisualStudio

Pour ce Projet, l'application GitHub à été utilisé
- Téléchargez le fichier d'installation « [GidHub.exe](https://desktop.github.com/) » (version 2024) sur le site : 

- Double-cliquez sur "Donwload Windows" dans l'onglet "Community".

- Suivez les instructions d'installations à l'écran pour procéder à l'installation de GitHub

## Introduction

Le format du code QR a été initié en 1994 par Denso-Wave, une filiale de Toyota spécialisée dans la fabrication de composants automobiles. La norme QR est définie par l'ISO/IEC 18004 :2006 et son utilisation est libre de licence. 

Les dimensions des codes QR varient de 21x21 pixels pour les plus petits à 177x177 pixels pour les plus grands, ce qui correspond à différentes versions. Par exemple, la version 1 correspond à 21x21 pixels et la version 40 correspond à 177x177 pixels. 

Les codes QR intègrent un mécanisme de correction d'erreurs : lors de l'encodage, des données redondantes sont ajoutées pour permettre à un lecteur QR de déchiffrer le code correctement même si certaines parties sont illisibles. 

Pour notre projet, nous nous concentrerons sur les deux premières versions des codes QR
## Cahier des charges

-	Générer un QR code a l’aide d’une phrase quelconque entrer par un utilisateur 
-	Générer les deux premiers types de QR code (21 x 21 et 25 x 25)
-	Afficher le message que cache un QR code que l’on récupère dans notre dossier d’image

## Etape de conception 

1)	Décomposition et conversion de la phrase en binaire, création du tableau de byte contenant toutes les informations nécessaire pour la création du QR code
Méthodes utilisées : 
-	TableauQRCODE
-	DecimalToBinary
-	CharToByteTab
-	BINAIRETOBYTE
-	ReedSolomonAlgorithm.Encode
-	ReedSolomonCreate
-	recherche_dichotomique_recursif

2)	Création de l’image et création des 3 carrés permettant de lire le QR code dans le bon sens ainsi que des pointillés reliant les carrés
Méthodes utilisées : 
-	CreationBase
-	SousMasque
-	Pointille

3)	Remplissage du QR code a l’aide de notre tableau de byte constituer de 0 et de 1 ainsi que le calcul du masque 0 ((i + j) % 2 = 0)
Méthodes utilisées : 
-	CreationBase
-	Remplissage
-	SurMasque
-	SurMasqueDeCode

4)	Test du QR code à l’aide du lecteur de notre téléphone
5)	Decodage d’une image QR Code donner dans les fichiers
Méthodes utilisées : 
-	Decode
-	Retrecir
-	DeCodageQrCode

## Interface utilisateur

-	Interface graphique conviviale pour l'utilisateur.
-	Fonctionnalités d'entrée pour saisir les données à encoder ou scanner les QR codes.

## Problèmes rencontrés

1)	Une phrase de longueur 2 et ayant un nombre de caractères multiple de trois ne permet pas de lire le QR Code. 
Solution : Ajouter une terminaison appropriée à toutes les longueurs de phrase.

2)	Adapter le masque 0 pour toutes les versions du QR Code.

3)	Erreur de remplissage de la matrice due à des sorties et aux motifs. 
Solution : Ajouter des conditions supplémentaires dans la méthode de remplissage.

4)	Les pixels ne sont pas remplis lorsque les données à insérer dépassent la capacité du tableau de données. 
Solution : Parcourir la matrice au lieu du tableau pour gérer les données excédentaires.

## Avis sur le projet

Le projet de génération de codes QR présenté est une initiative prometteuse qui cherche à mettre en œuvre les principes fondamentaux du codage QR pour créer des tableaux binaires conformes aux spécifications de différentes versions. Voici quelques points clés à considérer :

Points Forts :
-	Implémentation des Versions : Le projet démontre une compréhension approfondie des spécifications des différentes versions du codage QR. L'approche de gestion des versions et de génération de tableaux binaires en fonction de ces spécifications est bien structurée.
-	Gestion des Erreurs : La prise en compte de la correction d'erreurs à l'aide de l'algorithme de Reed-Solomon est un ajout important. Cela montre une préoccupation pour la fiabilité et l'intégrité des données, essentielles dans le contexte des codes QR.
-	Clarté du Code : Bien que la méthode TableauQRCODE soit complexe, elle est bien documentée et utilise des noms de variables explicites, facilitant la compréhension du fonctionnement du code.
-	Tests Unitaires : L'inclusion de tests unitaires, bien que limitée dans l'exemple, est un bon point. Les tests aident à valider le comportement attendu de la méthode et assurent une meilleure robustesse.

Suggestions d'Amélioration :
-	Extension des Tests : Il serait bénéfique d'élargir la couverture des tests unitaires pour inclure davantage de cas de test représentatifs pour chaque version du codage QR pris en charge.
-	Gestion des Exceptions : Considérez l'ajout de gestion explicite des exceptions pour rendre le code plus résilient face à des scénarios inattendus.
-	Optimisation : Évaluez les possibilités d'optimisation de la méthode TableauQRCODE pour améliorer les performances, en particulier pour les versions de codage QR plus grandes.


## Comment ça marche
C# utilise des [commandes](https://learn.microsoft.com/fr-fr/dotnet/csharp/) entre l'humain et l'ordinateur afin d'avoir de comprehensible si l'on apprend le language (comme du C++ ou du Java)

## Déploiement
  Pour exécuter le code du Projet,il suffit d'ouvrir le Dossier [C#](<https://fr.wikipedia.org/wiki/C_sharp#:~:text=C%23%20est%20un%20langage%20de,ou%20des%20biblioth%C3%A8ques%20de%20classes.(programming_language)&action=edit&redlink=1>) avec le logiciel [VisualStudio](https://visualstudio.microsoft.com/fr/downloads/), et de le lancé en appuyant sur le petit triange en haut à gauche ou à l'aide de la touche F7 

## Outils
- [C#](<https://fr.wikipedia.org/wiki/C_sharp#:~:text=C%23%20est%20un%20langage%20de,ou%20des%20biblioth%C3%A8ques%20de%20classes.(programming_language)&action=edit&redlink=1>)
- [MARKDOWN EDIT](<https://fr.wikipedia.org/wiki/Markdown>)
- [VisualStudio](https://visualstudio.microsoft.com/fr/downloads/)
- [GitHub](https://desktop.github.com/)
- [Support Microsoft](https://learn.microsoft.com/fr-fr/dotnet/csharp/)
- [Wikipedia](<https://fr.wikipedia.org/wiki/Code_QR>)
- [Thonky](<https://www.thonky.com/>)


## Crédits

Remerciement à Mme Ellul pour les indications utile lors des séances dédié au projet.


## Acknowledgements

 - [Awesome Readme Templates](https://awesomeopensource.com/project/elangosundar/awesome-README-templates)
 - [Awesome README](https://github.com/matiassingers/awesome-readme)
 - [How to write a Good readme](https://bulldogjob.com/news/449-how-to-write-a-good-readme-for-your-github-project)

