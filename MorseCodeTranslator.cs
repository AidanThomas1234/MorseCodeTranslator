using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System.Text;

public class dictionarys
{

    Dictionary<string, string> TranslationSet = new Dictionary<string, string>();
    string input;
    StringBuilder output = new StringBuilder();

    public void Menu()
    {
        //clearing loggin veribles so they can be used again 
        output.Clear();
        input = "";

        Console.WriteLine("\n**********************************\nChose one of the follwing options\n1)English-MorseCode\n2)Morse Code-English\n");
        string MenuUinputConsole = Console.ReadLine();

        if (MenuUinputConsole == "1")
        {
            ENGLANISHmorse();


        }
        else if (MenuUinputConsole == "2")
        {
            MORSEenglish();
        }
        else
        {
            Console.WriteLine("Invalid output");
            Menu();
        }
    }

    //turning Eglish to morse code, passing in dictionary "FILEandDICTIONARY"
    public void ENGLANISHmorse()

    {
        {
            FILEandDICTIONARY();

            Console.WriteLine("\nWrite the text that you would like to be translated into morse code\n");
            input = Console.ReadLine();

            // Split up the words characters individually so it can be read by the dictionary 
            input = input.ToUpper();
            string[] notInput = input.Split(' ');

            foreach (string a in notInput)
            {
                char[] letters = a.ToCharArray();

                foreach (char character in a)
                {
                    // Key is the user input, if it contains the key, then character to string 
                    if (TranslationSet.ContainsKey(character.ToString()))
                    {
                        Console.Write(TranslationSet[character.ToString()] + " ");
                        output.Append(TranslationSet[character.ToString()] + " ");
                    }
                    // Separates Morse code characters with spaces to make it readable
                    Console.Write(" ");
                    output.Append(" ");

                }

                // New line for different words
                Console.Write("/");
                output.Append("/");
            }




            //Loop back to menu( pervents restart after each translation )
            Console.WriteLine("\n\nStoring in \"outputs.txt\"...\n\nRerouting.....\n");
            LoggingOutputs();
            Menu();





        }
    }

    //sending outputs to a file morse.txt , english .txt
    public void LoggingOutputs()
    {


        //users origonal input, what was outputed 
        string data = $"{"*******************************"},{"\nOrigonal Input:"},{input},{"\nTranslated output:"},{output},{"\nDate and time:"},{DateTime.Now},{"\n"},{"**************************************"}";

        File.AppendAllText("Translation_log.txt", data);  // Create a file and write the content of writeText to it
    }


    public void MORSEenglish()
    {

        FILEandDICTIONARY();

        Console.WriteLine("Write the Morse code that you would like to be translated into English:");
        string USERinput = Console.ReadLine();



        // this takes the userinput and split it based on the / provided by th user 
        string[] morseWords = USERinput.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

        //string builder - a string we can add to , good for loops 
        List<string> EnglishWords = new List<string>();

        foreach (string morseWord in morseWords)
        {
            //takes the morse code letters into indivdual values to be translated 
            string[] morseLetters = morseWord.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder englishWord = new StringBuilder();

            foreach (string morseLetter in morseLetters)
            {
                string letter = TranslationSet.FirstOrDefault(x => x.Value == morseLetter).Key;
                if (!string.IsNullOrEmpty(letter))
                {
                    englishWord.Append(letter);
                    
                }
            }
            //adding the english word just translated to the english words previously translated in this instence 
            EnglishWords.Add(englishWord.ToString());
            
        }
       
        string English = string.Join(" ", EnglishWords);
        Console.WriteLine("English: " + English);
        output.Append(English);
        input = USERinput;

        LoggingOutputs();
        Menu();
    }




public void FILEandDICTIONARY()
    {

        Console.WriteLine("\n***************\nWhich would you like to do ?\n1)American\n2)Internatioal");

        //Error handling and determined the user intput 
        string Langauge = Console.ReadLine();


        //This will loop if invalid input 
        if (Langauge == "1")
        {
            Langauge = "american";


        }
        else if (Langauge == "2")
        {
            Langauge = "international";

        }
        else
        {
            Console.WriteLine("Invalid input retry");
            FILEandDICTIONARY();
        }




        //Loadin the file using $"(verible).txt so that i am able to use the user input to load the correct program  
        string allcontent = $"{Langauge}.txt";


        //read the lines into a string 
        string[] rows = File.ReadAllLines(allcontent);
        TranslationSet.Clear();


        //This section will iterate through the results and pass them into the created data dictionary
        foreach (string row in rows)
        {
            //this ignores lines with # starting
            if (!row.StartsWith('#'))
            {
                // Split by space character
                string[] delimiter = row.Split(' ');

                if (delimiter.Length >= 2)
                {

                    string letter = delimiter[0].ToUpper();
                    string morseCode = string.Join(" ", delimiter.Skip(1));
                    TranslationSet.Add(letter, morseCode);
                }
            }
        }


    }
}

class Program
{
    static void Main()
    {
        dictionarys dictionary = new dictionarys();
        // Call the Menu function
        dictionary.Menu();

    }
}

//https://github.com/aidanthomas1234/