using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class Word {

    public int id;
    public int callNumber;
    public int numberOfLetters;
    public string word;
    public string description;

    public Word(int id, int callNumber, int numberOfLetters, string word, string description)
    {
        this.id = id;
        this.callNumber = callNumber;
        this.numberOfLetters = numberOfLetters;
        this.word = word;
        this.description = description;
    }
    public Word()
    { }
}

