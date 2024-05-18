using System.Numerics;
using Image_Morganico_Dupin;

namespace Image_Morganico_Dupin
{

    public class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(@"
 _____________________________________________________
|                                                     |
|                                                     |
|                                                     |
|                                                     |
|               Projet MyImage & QRCode               |
|                                                     |
|                                                     |
|                                                     |
|_____________________________________________________|");

            Console.ForegroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 0;

            Console.WriteLine("\nNaviguez avec les flèches pour choisir une option. Appuyez sur Entrée pour sélectionner.\nChoisir une édition :\n");
            int top = Console.CursorTop;
            string[] options = { "Projet Image", "Projet QrCode", "Quitter" };

            while (!rep)
            {
                Console.SetCursorPosition(0, top);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.ForegroundColor = (i == option) ? ConsoleColor.Green : ConsoleColor.Magenta;
                    Console.WriteLine(options[i]);
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option >= options.Length)
                            option = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option < 0)
                            option = options.Length - 1;
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            switch (option)
            {
                case 0:
                    MenuMyImage(args);
                    Retour_MenuMyImage(args);
                    break;
                case 1:
                    MenuQrCode(args);
                    Retour_MenuMyImage(args);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Au revoir !");
                    break;
            }
            
        }
        static void Retour_MenuMyImage(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Appuyer pour revenir au menu");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Main(args);
        }

        static void MenuMyImage(string[] args)
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine(@"
 _____________________________________________________
|                                                     |
|                                                     |
|                                                     |
|                                                     |
|                   Projet MyImage                    |
|                                                     |
|                                                     |
|                                                     |
|_____________________________________________________|");
            Console.ForegroundColor = ConsoleColor.Green;
            string s = ("Bienvenue dans votre programme d'édition d'images !\n");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            Console.ResetColor();
            string nom_fichier = Choix_de_Fichier(args);
            MyImage image = new MyImage(nom_fichier);
            Choix_Mode(image,args);
        }

        static void MenuQrCode(string[] args)
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine(@"
 _____________________________________________________
|                                                     |
|                                                     |
|                                                     |
|                                                     |
|                  Projet QRCode                      |
|                                                     |
|                                                     |
|                                                     |
|_____________________________________________________|");
            Console.ForegroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 0;

            Console.WriteLine("\nNaviguez avec les flèches pour choisir une option. Appuyez sur Entrée pour sélectionner.\n\nChoisir une édition :\n");
            int top = Console.CursorTop;
            string[] options = { "Créer un QrCode", "Traduire un QrCode", "Quitter" };

            while (!rep)
            {
                Console.SetCursorPosition(0, top);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.ForegroundColor = (i == option) ? ConsoleColor.Green : ConsoleColor.Magenta;
                    Console.WriteLine(options[i]);
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option >= options.Length)
                            option = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option < 0)
                            option = options.Length - 1;
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            switch (option)
            {
                case 0:
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Veuillez entrer votre phrase d'une longueur inferieur a 155 caractères : \n");
                    string txt = Console.ReadLine();
                    while (txt.Length > 154)
                    {
                        Console.WriteLine("Veuillez entrer votre phrase d'une longueur inferieur a 155 caractères : \n");
                        txt = Console.ReadLine();
                    }
                    QRCode QR = new QRCode(txt);
                    break;
                case 1:
                    Console.Clear();
                    QRCode k = new QRCode();
                    k.Decode();
                    break;
                case 2:
                    Main(args);
                    break;
            }
        }
        static void Choix_Mode(MyImage image,string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 0;
            Console.WriteLine("\nNaviguer avec les flèches pour choisir l'option. Appuyer sur entrer une fois le choix fait\nChoisir son édition : \n");
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 0 ? color : "\u001b[35m")} Enregistrer\u001b[0m");
                Console.WriteLine($"{(option == 1 ? color : "\u001b[35m")} Agrandissement\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "\u001b[35m")} Reduction\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "\u001b[35m")} Noir et Blanc\u001b[0m");
                Console.WriteLine($"{(option == 4 ? color : "\u001b[35m")} Stéganographie\u001b[0m");
                Console.WriteLine($"{(option == 5 ? color : "\u001b[35m")} Mirroir\u001b[0m");
                Console.WriteLine($"{(option == 6 ? color : "\u001b[35m")} Convolution\u001b[0m");
                Console.WriteLine($"{(option == 7 ? color : "\u001b[35m")} Histogramme\u001b[0m");
                Console.WriteLine($"{(option == 8 ? color : "\u001b[35m")} Rotation\u001b[0m");
                Console.WriteLine($"{(option == 9 ? color : "\u001b[35m")} Fractale\u001b[0m");
                Console.WriteLine($"{(option == 10 ? color : "\u001b[35m")} Quittez\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == 11)
                        {
                            option = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == -1)
                        {
                            option = 10;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            switch (option)
            {
                case 0:
                    {
                        Console.Clear();
                        Engistrer_Copie(image);
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Agrandir L'image : \nChosir un facteur entre 1 et 10");
                        int facteur = -1;
                        while (facteur < 1) facteur = Convert.ToInt32(Console.ReadLine());
                        image.Agrandir(facteur);
                        image.From_Image_To_File("Agrandis.bmp");
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 2:
                    {
                        Console.Clear();
                        List<int> liste = Diviseurs_Communs(image.Height, image.Width);
                        Console.WriteLine("Reduire l'image : \nChoisir un Diviseur entre");
                        int facteur = Choix_Facteur(liste);
                        image.Retrecir(facteur);
                        image.From_Image_To_File("Retrecis.bmp");
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 3:
                    {
                        Console.Clear();
                        Console.WriteLine("Nuance Gris : \nTranformation en NuanceGris");
                        image.NuanceGris();
                        image.From_Image_To_File("NuanceGris.bmp");
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 4:
                    {
                        Console.Clear();
                        Console.WriteLine("Superposition :");
                        image.Steganographie(Choix_de_Fichier(args));
                        image.From_Image_To_File("SteganographieCodage.bmp");
                        image.DeSteganographie();
                        image.From_Image_To_File("SteganographieDeCodage.bmp");
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 5:
                    {
                        Console.Clear();
                        Console.WriteLine("Mirroir :");
                        image.Mirroir();
                        image.From_Image_To_File("Mirroir.bmp");
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 6:
                    {
                        Console.Clear();
                        Console.WriteLine("Appliquer une matrice de convolution :\n");
                        int[,] matrice = Matrice_De_Convolution();
                        image.Convolution(matrice);
                        image.From_Image_To_File("Convultion.bmp");
                        Console.WriteLine("Réussis");
                        Retour_Menu(image, args);
                    }
                    break;
                case 7:
                    {
                        Console.Clear();
                        Console.WriteLine("Histogramme");
                        image.histogramme();
                        image.From_Image_To_File("Histogramme.bmp");
                        Retour_Menu(image, args);
                    }
                    break;
                case 8:
                    {
                        Console.Clear();
                        Console.WriteLine("Choisir l'angle :");
                        image.Rotation(int.Parse(Console.ReadLine()));
                        image.From_Image_To_File("Rotation.bmp");
                        Retour_Menu(image, args);
                    }
                    break;
                case 9:
                    {
                        Console.Clear();
                        MyImage fractale = new MyImage("Fractale.bmp");
                        bool etat = bool_ChoixFractale();
                        double[] valeur = Val_ChoixFractale();
                        fractale.Fractale(etat, valeur[0], valeur[1]);
                        fractale.From_Image_To_File("Fractale.bmp");
                        Retour_Menu(image,args);
                    }
                    break;
                case 10:
                    {
                        MenuMyImage(args);
                    }
                    break;
            }

        }
        static int Choix_Facteur(List<int> liste)
        {
            bool etat = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 1;
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                for (int i = 0; i < liste.Count; i++)
                {
                    Console.WriteLine($"{(option == i + 1 ? color : "\u001b[35m")}{liste[i]}\u001b[0m");
                }
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == liste.Count)
                        {
                            option = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == 0)
                        {
                            option = liste.Count - 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            return liste[option - 1];
        }
        static bool bool_ChoixFractale()
        {
            bool etat = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 1;
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? color : "\u001b[35m")} Version 1\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "\u001b[35m")} Version 2\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == 3)
                        {
                            option = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == 0)
                        {
                            option = 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            if (option == 2)
            {
                etat = true;
            }
            return etat;
        }
        static double[] Val_ChoixFractale()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            double[] reponse = new double[2];
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 1;
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? color : "\u001b[35m")} 0.285+0.01i\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "\u001b[35m")} 0.5+0.5i\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "\u001b[35m")} 0.8 + 0.156i\u001b[0m");
                Console.WriteLine($"{(option == 4 ? color : "\u001b[35m")} 1.754+0.038i\u001b[0m");
                Console.WriteLine($"{(option == 5 ? color : "\u001b[35m")} 0.70176−0.3842i\u001b[0m");
                Console.WriteLine($"{(option == 6 ? color : "\u001b[35m")} 0.162+1.04i\u001b[0m");
                Console.WriteLine($"{(option == 7 ? color : "\u001b[35m")} 0.123+0.745i\u001b[0m");
                Console.WriteLine($"{(option == 8 ? color : "\u001b[35m")} 0.743+0.1i\u001b[0m");
                Console.WriteLine($"{(option == 9 ? color : "\u001b[35m")} 0.637+0.42i\u001b[0m");
                Console.WriteLine($"{(option == 10 ? color : "\u001b[35m")} 0.59+0.62i\u001b[0m");
                Console.WriteLine($"{(option == 11 ? color : "\u001b[35m")} 0.726895347709114071439+0.188887129043845954792i\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == 13)
                        {
                            option = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == 0)
                        {
                            option = 12;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            switch (option)
            {
                case 1:
                    reponse[0] = 0.285;
                    reponse[1] = 0.01;
                    ;
                    break;
                case 2:
                    reponse[0] = 0.5;
                    reponse[1] = 0.5;
                    ;
                    break;
                case 3:
                    reponse[0] = 0.8;
                    reponse[1] = 0.156;
                    ;
                    break;
                case 4:
                    reponse[0] = 1.754;
                    reponse[1] = 0.038;
                    ;
                    break;
                case 5:
                    reponse[0] = 0.70176;
                    reponse[1] = -0.3842;
                    ;
                    break;
                case 6:
                    reponse[0] = 0.162;
                    reponse[1] = 1.04;
                    ;
                    break;
                case 7:
                    reponse[0] = 0.123;
                    reponse[1] = 0.745;
                    ;
                    break;
                case 8:
                    reponse[0] = 0.743;
                    reponse[1] = 0.1;
                    ;
                    break;
                case 9:
                    reponse[0] = 0.637;
                    reponse[1] = 0.42;
                    ;
                    break;
                case 10:
                    reponse[0] = 0.59;
                    reponse[1] = 0.62;
                    ;
                    break;
                case 11:
                    reponse[0] = 0.726895347709114071439;
                    reponse[1] = 0.188887129043845954792;
                    ;
                    break;
            }

            return reponse;
        }
        static int[,] Matrice_De_Convolution()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string reponse = "";
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 1;
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? color : "\u001b[35m")} Flou\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "\u001b[35m")} Améliorations des bords\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "\u001b[35m")} Augmenter le contraste\u001b[0m");
                Console.WriteLine($"{(option == 4 ? color : "\u001b[35m")} Détection des bords\u001b[0m");
                Console.WriteLine($"{(option == 5 ? color : "\u001b[35m")} Repoussage\u001b[0m");
                Console.WriteLine($"{(option == 6 ? color : "\u001b[35m")} Filtre de Sobel\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == 7)
                        {
                            option = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == 0)
                        {
                            option = 6;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            int[,] matrice = null;
            switch (option)
            {
                case 1:
                    matrice = new int[,]
                    {{0, 0, 100, 0, 0},
             {0, 100, 50, 100, 0},
             {100, 50, 5, 50, 100},
             {0, 100, 50, 100, 0},
             {0, 0, 100, 0, 0}
                    };
                    break;
                case 2:
                    matrice = new int[,]
                    {
            {0, 0, 0},
            {-1, 1, 0},
            {0, 0, 0}
                    };
                    break;
                case 3:
                    matrice = new int[,]
                    {
            {0, -1, 0},
            {-1, 5, -1},
            {0, -1, 0}
                    };
                    break;
                case 4:
                    matrice = new int[,]
                    {
            {0, 1, 0},
            {1, -4, 1},
            {0, 1, 0}
                    };
                    break;
                case 5:
                    matrice = new int[,]
                    {
            {-2, -1, 0},
            {-1, 1, 2},
            {0, 2, 2}
                    };
                    break;
                case 6:
                    matrice = new int[,]
                    {
            {-1, 0, 1},
            {-2, 0, 2},
            {-1, 0, 0}
                    };
                    break;
            }

            return matrice;

        }
        static void Retour_Menu(MyImage image, string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Appuyer pour revenir au menu");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Choix_Mode(image,args);
        }
        static string Choix_de_Fichier(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string Chaine_de_caractère = "";
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 1;
            Console.WriteLine("\nNaviguer avec les flèches pour choisir l'option. Appuyer sur entrer une fois le choix fait\nChoisir son image : \n");
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m"; // Couleur verte
            string resetColor = "\u001b[0m"; // Réinitialiser la couleur
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? color : "\u001b[35m")} Zelda{resetColor}");
                Console.WriteLine($"{(option == 2 ? color : "\u001b[35m")} Vangogh{resetColor}");
                Console.WriteLine($"{(option == 3 ? color : "\u001b[35m")} Poisson{resetColor}");
                Console.WriteLine($"{(option == 4 ? color : "\u001b[35m")} Plage{resetColor}");
                Console.WriteLine($"{(option == 5 ? color : "\u001b[35m")} Parapluies{resetColor}");
                Console.WriteLine($"{(option == 6 ? color : "\u001b[35m")} Montagne{resetColor}");
                Console.WriteLine($"{(option == 7 ? color : "\u001b[35m")} Mario{resetColor}");
                Console.WriteLine($"{(option == 8 ? color : "\u001b[35m")} Lena{resetColor}");
                Console.WriteLine($"{(option == 9 ? color : "\u001b[35m")} Lac en montagne{resetColor}");
                Console.WriteLine($"{(option == 10 ? color : "\u001b[35m")} Joconde{resetColor}");
                Console.WriteLine($"{(option == 11 ? color : "\u001b[35m")} Coco{resetColor}");
                Console.WriteLine($"{(option == 12 ? color : "\u001b[35m")} Chaton{resetColor}");
                Console.WriteLine($"{(option == 13 ? color : "\u001b[35m")} Cascade{resetColor}");
                Console.WriteLine($"{(option == 14 ? color : "\u001b[35m")} Automne{resetColor}");
                Console.WriteLine($"{(option == 15 ? color : "\u001b[35m")} Quitter{resetColor}");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == 16)
                        {
                            option = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == 0)
                        {
                            option = 15;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            switch (option)
            {
                case 1:
                    Chaine_de_caractère = "zelda.bmp";
                    break;
                case 2:
                    Chaine_de_caractère = "vangogh.bmp";
                    break;
                case 3:
                    Chaine_de_caractère = "poisson.bmp";
                    break;
                case 4:
                    Chaine_de_caractère = "plage.bmp";
                    break;
                case 5:
                    Chaine_de_caractère = "parapluies.bmp";
                    break;
                case 6:
                    Chaine_de_caractère = "montagne.bmp";
                    break;
                case 7:
                    Chaine_de_caractère = "mario.bmp";
                    break;
                case 8:
                    Chaine_de_caractère = "lena.bmp";
                    break;
                case 9:
                    Chaine_de_caractère = "lac_en_montagne.bmp";
                    break;
                case 10:
                    Chaine_de_caractère = "joconde.bmp";
                    break;
                case 11:
                    Chaine_de_caractère = "coco.bmp";
                    break;
                case 12:
                    Chaine_de_caractère = "chaton.bmp";
                    break;
                case 13:
                    Chaine_de_caractère = "cascade.bmp";
                    break;
                case 14:
                    Chaine_de_caractère = "automne.bmp";
                    break;
                case 15:
                    Main(args);
                    break;
            }
            Console.WriteLine("Fichier choisi : " + Chaine_de_caractère);
            return Chaine_de_caractère;
        }

        static void Engistrer_Copie(MyImage myimage)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string reponse = "";
            ConsoleKeyInfo key;
            bool rep = false;
            int option = 1;
            Console.WriteLine("Voulez-vous enregistrer une copie ? \n");
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";
            while (!rep)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? color : "\u001b[35m")} Oui\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "\u001b[35m")} Non\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option++;
                        if (option == 3)
                        {
                            option = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        option--;
                        if (option == 0)
                        {
                            option = 2;
                        }
                        break;
                    case ConsoleKey.Enter:
                        rep = true;
                        break;
                }
            }
            if (option == 1)
            {
                string fichier = "";
                while (true)
                {
                    Console.WriteLine("Entrer le nom du fichier");
                    fichier = Console.ReadLine();
                    Console.WriteLine("Re-entrer le nom pour confirmé");
                    string re_fichier = Console.ReadLine();
                    if (fichier == re_fichier) break;
                }
                myimage.From_Image_To_File(fichier + ".bmp");
            }
        }
        static List<int> Diviseurs_Communs(int a, int b)
        {
            List<int> diviseursCommuns = new List<int>();

            for (int i = 1; i <= Math.Min(a, b); i++)
            {
                if (a % i == 0 && b % i == 0)
                {
                    diviseursCommuns.Add(i);
                }
            }

            return diviseursCommuns;
        }
    }
}