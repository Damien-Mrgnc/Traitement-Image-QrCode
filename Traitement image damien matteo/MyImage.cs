namespace Image_Morganico_Dupin
{
    public class MyImage
    {
        private string Upimage;
        private int tfichier;
        private int toffset;
        private int tailleentete;
        private int largeur;
        private int hauteur;
        private int nb_bit_couleur;
        private Pixel[,] matrice;
       
        public MyImage()
        {
            this.Upimage = "BM";
            this.toffset = 54;
            this.largeur = 1000;
            this.hauteur = 1000;
            this.nb_bit_couleur = 24;
            this.tfichier = toffset + largeur * hauteur * 3;
            this.matrice = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++) for (int j = 0; j < largeur; j++) matrice[i, j] = new Pixel(0, 0, 0);
            this.tailleentete = 40;
        }
        public MyImage(string nomfichier)
        {
            try
            {
                byte[] file = File.ReadAllBytes(nomfichier);
                this.Upimage = "" + Convert.ToChar(file[0]) + Convert.ToChar(file[1]);
                this.tfichier = Convertir_Endian_To_Int(file, 2, 5);
                this.toffset = Convertir_Endian_To_Int(file, 10, 13);
                this.tailleentete = Convertir_Endian_To_Int(file, 14, 17);
                this.largeur = Convertir_Endian_To_Int(file, 18, 21); ;
                this.hauteur = Convertir_Endian_To_Int(file, 22, 25);
                this.nb_bit_couleur = Convertir_Endian_To_Int(file, 28, 29);
                Int64 indice = toffset;
                matrice = new Pixel[hauteur, largeur];
                for (int j = hauteur - 1; j >= 0; j--)
                {
                    for (int k = 0; k < largeur; k++)
                    {
                        matrice[j, k] = new Pixel(file[indice], file[indice + 1], file[indice + 2]);
                        indice += 3;
                    }
                }
            }
            catch (Exception ex)
            {
                this.toffset = 0;
            }
        }
        public string Image { get { return Upimage; } set { Upimage = value; } }
        public Pixel[,] Matrice { get { return matrice; } set { matrice = value; } }
        public int Width { get { return largeur; } set { largeur = value; } }
        public int Height { get { return hauteur; } set { hauteur = value; } }
        public int TailleFichier { get { return tfichier; }set { tfichier = value; } }
        public int Offset { get { return toffset; } set {  toffset = value; } }
        public void From_Image_To_File(string files)
        {
            byte[] Tab = new byte[largeur * hauteur * 3 + toffset];

            Tab[0] = Convert.ToByte(Upimage[0]);
            Tab[1] = Convert.ToByte(Upimage[1]);

            byte[] remplaçant = Convertir_Int_To_Endian(tfichier, 4);
            int indicefin = 2;
            for (int i = 0; i < 4; i++)
            {
                Tab[indicefin + i] = remplaçant[i];
            }

            indicefin = 10;
            remplaçant = Convertir_Int_To_Endian(toffset, 4);
            for (int i = 0; i < 4; i++)
            {
                Tab[indicefin + i] = remplaçant[i];
            }

            indicefin = 14;
            remplaçant = Convertir_Int_To_Endian(tailleentete, 4);
            for (int i = 0; i < 4; i++)
            {
                Tab[indicefin + i] = remplaçant[i];
            }

            indicefin = 18;
            remplaçant = Convertir_Int_To_Endian(largeur, 4);
            for (int i = 0; i < 4; i++)
            {
                Tab[indicefin + i] = remplaçant[i];
            }

            indicefin = 22;
            remplaçant = Convertir_Int_To_Endian(hauteur, 4);
            for (int i = 0; i < 4; i++)
            {
                Tab[indicefin + i] = remplaçant[i];
            }

            indicefin = 28;
            remplaçant = Convertir_Int_To_Endian(nb_bit_couleur, 2);
            for (int i = 0; i < 2; i++)
            {
                Tab[indicefin + i] = remplaçant[i];
            }

            indicefin = toffset;
            for (int i = hauteur - 1; i >= 0; i--)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (matrice[i, j] != null)
                    {
                        Tab[indicefin] = matrice[i, j].Bleu;
                        Tab[indicefin + 1] = matrice[i, j].Vert;
                        Tab[indicefin + 2] = matrice[i, j].Rouge;
                    }
                    //else Console.WriteLine("Pixel null");
                    indicefin += 3;
                }
            }
            //for(int i=0; i< 50; i++) Console.Write(Tab[i]);
            Console.WriteLine();
            File.WriteAllBytes(files, Tab);
        }
        public int Convertir_Endian_To_Int(byte[] e, int debut, int fin)
        {
            byte[] copie = new byte[fin - debut + 1];
            int n = 0;
            for (int i = debut; i < fin; i++)
            {
                copie[n] = e[i];
                n++;
            }
            int somme = 0;
            for (int i = 0; i < copie.Length; i++)
            {
                somme += (int)(copie[i] * Math.Pow(256, i));
            }
            return somme;
        }
        public byte[] Convertir_Int_To_Endian(int val, int taille)
        {
            byte[] bytes = new byte[taille];
            for (int i = 0; i < taille; i++)
            {
                bytes[i] = (byte)(val >> (i * 8));
            }
            return bytes;
        }
        /// <summary>
        /// assigne une nouvelle largeur et hauteur en multipliant celle de base par le facteur et rempli la nouvelle matrice
        /// </summary>
        /// <param name="facteur">chiffre par lequel va etre aggrandi l'image</param>
        public void Agrandir(int facteur)
        {
            int nouvelleLargeur = largeur * facteur;
            int nouvelleHauteur = hauteur * facteur;

            Pixel[,] nouvelleMatrice = new Pixel[nouvelleHauteur, nouvelleLargeur];
            for (int i = 0; i < nouvelleHauteur; i++)
            {
                for (int j = 0; j < nouvelleLargeur; j++)
                {
                    int originalHauteur = i / facteur;
                    int originalLargeur = j / facteur;

                    nouvelleMatrice[i, j] = matrice[originalHauteur, originalLargeur];
                }
            }

            largeur = nouvelleLargeur;
            hauteur = nouvelleHauteur;
            tfichier = toffset + largeur * hauteur * 3;
            matrice = nouvelleMatrice;
        }
        /// <summary>
        /// assigne une nouvelle largeur et hauteur en multipliant celle de base par le facteur et rempli la nouvelle matrice
        /// </summary>
        /// <param name="facteur">chiffre par lequel l'image va etre retrecie</param>
        public void Retrecir(int facteur)
        {
            int nouvelleLargeur = largeur / facteur;
            int nouvelleHauteur = hauteur / facteur;

            Pixel[,] nouvelleMatrice = new Pixel[nouvelleHauteur, nouvelleLargeur];
            for (int i = 0; i < nouvelleHauteur; i++)
            {
                for (int j = 0; j < nouvelleLargeur; j++)
                {
                    int originalHauteur = i * facteur;
                    int originalLargeur = j * facteur;

                    nouvelleMatrice[i, j] = matrice[originalHauteur, originalLargeur];
                }
            }

            largeur = nouvelleLargeur;
            hauteur = nouvelleHauteur;
            tfichier = toffset + largeur * hauteur * 3;
            matrice = nouvelleMatrice;
        }
        /// <summary>
        /// fais la somme des RGB et et la divise par trois ce qui va donner des nuance de gris
        /// </summary>
        public void NuanceGris()
        {
            Pixel[,] NouvelleMatrice = new Pixel[matrice.GetLength(0), matrice.GetLength(1)];
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    double r = matrice[i, j].Rouge;
                    double v = matrice[i, j].Vert;
                    double b = matrice[i, j].Bleu;
                    double somme = (r + v + b) / 3;
                    NouvelleMatrice[i, j] = new Pixel((byte)somme, (byte)somme, (byte)somme);
                }

            }
            matrice = NouvelleMatrice;
        }
        /// <summary>
        /// va prendre les bit de poids fort de l'image passer en parametre et les echanger avec les bit de poids faible de l'image de base
        /// </summary>
        /// <param name="nom">image caché dans celle de base</param>
        public void Steganographie(string nom)
        {
            MyImage myImage = new MyImage(nom);

            Pixel[,] nouvelleMatrice = new Pixel[hauteur, largeur];

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    string r = DecimalToBinary(matrice[i, j].Rouge);
                    string v = DecimalToBinary(matrice[i, j].Vert);
                    string b = DecimalToBinary(matrice[i, j].Bleu);
                    string imr = "";
                    string imv = "";
                    string imb = "";
                    if (i < myImage.Matrice.GetLength(0) && j < myImage.Matrice.GetLength(1))
                    {
                        imr = DecimalToBinary(myImage.Matrice[i, j].Rouge);
                        imv = DecimalToBinary(myImage.Matrice[i, j].Vert);
                        imb = DecimalToBinary(myImage.Matrice[i, j].Bleu);
                    }
                    else
                    {
                        imr = r; imv = v; imb = b;
                    }
                    byte bleu = BinaryToDecimal(AssemblageBinaire(b, imb));
                    byte vert = BinaryToDecimal(AssemblageBinaire(v, imv));
                    byte rouge = BinaryToDecimal(AssemblageBinaire(r, imr));
                    nouvelleMatrice[i, j] = new Pixel(bleu, vert, rouge);
                }
            }
            matrice = nouvelleMatrice;
        }
        /// <summary>
        /// va faire ressortir l'image cacher 
        /// </summary>
        public void DeSteganographie()
        {
            Pixel[,] nouvelleMatrice = new Pixel[hauteur, largeur];

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    string r = DecimalToBinary(matrice[i, j].Rouge).Substring(4) + DecimalToBinary(matrice[i, j].Rouge).Substring(0, 4);
                    string v = DecimalToBinary(matrice[i, j].Vert).Substring(4) + DecimalToBinary(matrice[i, j].Vert).Substring(0, 4);
                    string b = DecimalToBinary(matrice[i, j].Bleu).Substring(4) + DecimalToBinary(matrice[i, j].Bleu).Substring(0, 4);
                    nouvelleMatrice[i, j] = new Pixel(BinaryToDecimal(b), BinaryToDecimal(v), BinaryToDecimal(r));
                }
            }
            matrice = nouvelleMatrice;
        }
        /// <summary>
        /// forme un octet contenant les bit de poids fort de l'image de base et ajoute les bit de foid fort de l'autre octet
        /// </summary>
        /// <param name="ptsforts">octet auquel on garde les bit de poids fort</param>
        /// <param name="ptsfaibles">octet auquel on retire les bit de poids fort</param>
        /// <returns>octet final</returns>
        public string AssemblageBinaire(string ptsforts, string ptsfaibles)
        {
            string k = ptsforts.Substring(0, 4) + ptsfaibles.Substring(0, 4);
            return k;
        }
        /// <summary>
        /// converti un nombre decimal en octet
        /// </summary>
        /// <param name="decimalNumber">nombre a convertir</param>
        /// <returns>octet</returns>
        public string DecimalToBinary(int decimalNumber)
        {
            bool isNegative = decimalNumber < 0;
            int absValue = Math.Abs(decimalNumber);
            if (absValue == 0)
            {
                return "00000000";
            }
            string binary = "";
            while (absValue > 0)
            {
                binary = (absValue % 2) + binary;
                absValue /= 2;
            }
            while (binary.Length < 8)
            {
                binary = "0" + binary;
            }
            return binary;
        }
        /// <summary>
        /// converti un octet en nombre decimal
        /// </summary>
        /// <param name="binary">octet a convrtir</param>
        /// <returns>nombre en decimal</returns>
        public byte BinaryToDecimal(string binary)
        {
            if (binary == null || binary == "00000000") return 0;

            byte somme = 0;
            for (int i = 0; i < binary.Length; i++)
            {
                int bitValue;
                if (binary[binary.Length - 1 - i] == '1')
                {
                    bitValue = 1;
                }
                else
                {
                    bitValue = 0;
                }
                somme += (byte)(bitValue * Math.Pow(2, i));
            }
            return somme;
        }
        /// <summary>
        /// Creer une fractale de nuance bleu a partir d'une combinaison préalablement choisi, elle va etre former a l'aide des suite arithmetique.
        /// tant que la la valeur de notre suite reste inferieur a une valeur predefini (ici 100) et que sont module est lui inferieur a 4, alors nous augmentons la suite.
        /// si la valeur de notre suite est egal a la valeur predefini, alors le pixel deviens noir sinon il est coloré.
        /// </summary>
        /// <param name="nodiff">detect quel fractal faire entre les types existante</param>
        /// <param name="x">parti réel de notre complexe</param>
        /// <param name="y">parti imaginaire de notre complexe</param>
        public void Fractale(bool nodiff, double x, double y)
        {
            Random rnd = new Random();
            double taille = 2000;
            Pixel[,] NouvelleMatrice = new Pixel[(int)taille, (int)taille];
            int valeur = 100;
            byte r = (byte)(3 * valeur % 256);
            byte v = (byte)(1 * valeur % 256);
            byte b = (byte)(10 * valeur % 256);
            for (double i = 0; i < taille; i++)
            {
                for (double j = 0; j < taille; j++)
                {
                    Imaginaire z = new Imaginaire((i - taille / 2) / (taille / 2), (j - taille / 2) / (taille / 2));
                    Imaginaire c = new Imaginaire(x, y);
                    if (!nodiff)
                    {
                        Imaginaire permut = z;
                        z = c;
                        c = permut;
                    }
                    int somme = 1;
                    while (somme < valeur && z.ModuleCarre() < 4)
                    {
                        Imaginaire Zprime = z.Multiplication(z).Addition(c);
                        z = Zprime;
                        somme++;
                    }
                    if (somme == valeur)
                    {
                        NouvelleMatrice[(int)i, (int)j] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                        NouvelleMatrice[(int)i, (int)j] = new Pixel((byte)(somme * 255 / valeur * 2), 0, 0);
                    }
                }
            }
            hauteur = (int)taille;
            largeur = (int)taille;
            tfichier = toffset + largeur * hauteur * 3;
            matrice = NouvelleMatrice;

        }
        /// <summary>
        /// Echange la place des pixel de droite avec ceux de gauche
        /// </summary>
        public void Mirroir()
        {

            Pixel[,] NouvelleMatrice = new Pixel[matrice.GetLength(0), matrice.GetLength(1)];
            for (int i = 0; i < NouvelleMatrice.GetLength(0); i++)
            {
                for (int j = 0; j < NouvelleMatrice.GetLength(1); j++)
                {
                    NouvelleMatrice[i, j] = matrice[i, matrice.GetLength(1) - j - 1];
                }
            }
            matrice = NouvelleMatrice;
        }
        /// <summary>
        /// Converti l'angle en radian
        /// Calcule a l'aide de la trigonometrie le nouvelle dimensions de notre image, si la largeur n'est pas un multiple de 4, alors elle augmente de 1.
        /// Defini CentreX et centreY pour que l'image soit au milieu de la nouvelle image.
        /// A l'aide de la matrice de rotation nous allons pouvoir definir l'amplacement du pixel selon la nouvelle matrice
        /// </summary>
        /// <param name="angle">angle de rotation de l'image</param>
        public void Rotation(double angle)

        {
            angle *= Math.PI / 180.0;
            double[,] MatriceRotation = { { Math.Cos(angle), -Math.Sin(angle) }, { Math.Sin(angle), Math.Cos(angle) } };
            int nouvellehauteur = (int)(Math.Abs(Math.Cos(angle)) * hauteur + Math.Abs(Math.Sin(angle) * largeur));
            int nouvellelargueur = (int)(Math.Abs(Math.Cos(angle)) * largeur + Math.Abs(Math.Sin(angle) * hauteur));
            while (nouvellelargueur % 4 != 0) nouvellelargueur++;
            Pixel[,] NouvelleMatrice = new Pixel[nouvellehauteur, nouvellelargueur];
            int centreX = nouvellehauteur / 2;
            int centreY = nouvellelargueur / 2;
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    int x = (int)(MatriceRotation[0, 0] * (i - hauteur / 2) + MatriceRotation[0, 1] * (j - largeur / 2) + centreX);
                    int y = (int)(MatriceRotation[1, 0] * (i - hauteur / 2) + MatriceRotation[1, 1] * (j - largeur / 2) + centreY);
                    if (x >= 0 && x < NouvelleMatrice.GetLength(0) && y >= 0 && y < NouvelleMatrice.GetLength(1))
                    {
                        NouvelleMatrice[x, y] = matrice[i, j];
                    }
                }
            }
            largeur = nouvellelargueur;
            hauteur = nouvellehauteur;
            tfichier = toffset + largeur * hauteur * 3;
            matrice = NouvelleMatrice;
        }
        public void Rotation90()
        {
            Pixel[,] mat = new Pixel[matrice.GetLength(1), matrice.GetLength(0)];

            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    mat[j, matrice.GetLength(0) - i - 1] = matrice[i, j];
                }
            }
            largeur = matrice.GetLength(0);
            hauteur = matrice.GetLength(0);
            tfichier = toffset + largeur * hauteur * 3;
            matrice = mat;
        }
        /// <summary>
        /// cette méthode applique une opération de convolution à une image représentée par une matrice de pixels en utilisant une matrice de convolution spécifiée, puis renvoie une nouvelle image 
        /// </summary>
        /// <param name="mat">matrice de convolution</param>
        public void Convolution(int[,] mat)
        {
            int lMat = mat.GetLength(1);
            int hMat = mat.GetLength(0);
            Pixel[,] nouvelleMatrice = new Pixel[hauteur, largeur];
            int facteur = 0;
            for (int i = 0; i < lMat; i++)
            {
                for (int j = 0; j < hMat; j++)
                {
                    facteur += mat[i, j];
                }
            }
            if (facteur == 0) facteur = 1;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    int rouge = 0, vert = 0, bleu = 0;

                    for (int ki = 0; ki < hMat; ki++)
                    {
                        for (int kj = 0; kj < lMat; kj++)
                        {
                            int x = Math.Min(Math.Max(i - hMat / 2 + ki, 0), hauteur - 1);
                            int y = Math.Min(Math.Max(j - lMat / 2 + kj, 0), largeur - 1);
                            Pixel pixel = matrice[x, y];
                            rouge += (pixel.Rouge) * mat[ki, kj];
                            vert += (pixel.Vert) * mat[ki, kj];
                            bleu += (pixel.Bleu) * mat[ki, kj];
                        }
                    }

                    nouvelleMatrice[i, j] = new Pixel(
                        (byte)Math.Max(Math.Min(bleu / facteur, 255), 0),
                        (byte)Math.Max(Math.Min(vert / facteur, 255), 0),
                        (byte)Math.Max(Math.Min(rouge / facteur, 255), 0));
                }
            }
            matrice = nouvelleMatrice;
        }
        /// <summary>
        /// calcule l'histogramme d'une image en séparant les valeurs de Rouge, Vert et Bleu, puis en affichant ces histogrammes dans une nouvelle matrice de pixels sous forme de graphique
        /// </summary>
        public void histogramme()
        {
            int[] histoRouge = new int[largeur];
            int[] histoVert = new int[largeur];
            int[] histoBleu = new int[largeur];

            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    histoRouge[j] += (int)matrice[i, j].Rouge;
                    histoVert[j] += (int)matrice[i, j].Vert;
                    histoBleu[j] += (int)matrice[i, j].Bleu;
                }
            }

            Pixel[,] nouvelleMatrice = new Pixel[hauteur, largeur];
            int facteur = hauteur / 200;
            for (int i = 0; i < largeur; i++)
            {
                int red = histoRouge[i] / hauteur;
                int green = histoVert[i] / hauteur;
                int blue = histoBleu[i] / hauteur;

                nouvelleMatrice[Math.Min(red * facteur, hauteur - 1), i] = new Pixel(0, 0, 255);
                nouvelleMatrice[Math.Min(green * facteur, hauteur - 1), i] = new Pixel(0, 255, 0);
                nouvelleMatrice[Math.Min(blue * facteur, hauteur - 1), i] = new Pixel(255, 0, 0);
                for (int j = 0; j < hauteur; j++)
                {
                    if (nouvelleMatrice[j, i] == null)
                    {
                        nouvelleMatrice[j, i] = new Pixel(0, 0, 0);
                    }
                }


            }
            tfichier = toffset + nouvelleMatrice.Length * 3;
            matrice = nouvelleMatrice;

        }
    }
}

