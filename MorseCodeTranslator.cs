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
                    Console.Write(TranslationSet[character.ToString()] + "");
                    output.Append(TranslationSet[character.ToString()] + "");
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

    //sending outputs to a file morse.txt , english .txt
    public void LoggingOutputs()
    {


        //users origonal input, what was outputed 
        string data = $"{input},{output},{DateTime.Now}";
       
        File.WriteAllText("Translation_log.txt", data);  // Create a file and write the content of writeText to it
    }


    public void MORSEenglish()
    {
        Console.WriteLine("Write the text that you would like to be translated into English\n");
        string InputedMorseCode = Console.ReadLine();
        Console.WriteLine("You entered:", InputedMorseCode);

        string[] morseInput = InputedMorseCode.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

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



        //This section will iterate through the results and pass them into the created data dictionary
        foreach (string row in rows)
        {
            //this ignores lines with # starting
            if (!row.StartsWith('#'))
            {
                // Split by space character
                string[] delimiter = row.Split(' ');

                if (delimiter.Length == 2)
                {
                    //0 meaning the first collum and 1 meaning the ssecond this iterates "key" and "valu" as them 
                    string Key = delimiter[0].Trim();
                    string Value = delimiter[1].Trim();
                    TranslationSet[Key] = Value;
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