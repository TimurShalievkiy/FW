using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour {
    public List<Word> words;
    // Use this for initialization
    public Animals()
    {
        words = new List<Word>();

        words.Add(new Word { id = 0, callNumber = 1, numberOfLetters = 6, word = "собака", description = "собака описание" });
        words.Add(new Word { id = 1, callNumber = 2, numberOfLetters = 3, word = "кот", description = "кот описание" });
        words.Add(new Word { id = 2, callNumber = 0, numberOfLetters = 5, word = "жираф", description = "жираф описание" });
        words.Add(new Word { id = 3, callNumber = 0, numberOfLetters = 4, word = "слон", description = "слон описание" });
        words.Add(new Word { id = 4, callNumber = 1, numberOfLetters = 7, word = "бегемот", description = "бегемот описание" });
        words.Add(new Word { id = 5, callNumber = 0, numberOfLetters = 5, word = "зебра", description = "зебра описание" });
        words.Add(new Word { id = 6, callNumber = 1, numberOfLetters = 4, word = "мышь", description = "мышь описание" });
        words.Add(new Word { id = 7, callNumber = 2, numberOfLetters = 6, word = "борсук", description = "борсук описание" });
        words.Add(new Word { id = 8, callNumber = 1, numberOfLetters = 3, word = "бык", description = "бык описание" });
        words.Add(new Word { id = 9, callNumber = 1, numberOfLetters = 5, word = "козел", description = "козел описание" });
        words.Add(new Word { id = 10, callNumber = 0, numberOfLetters = 5, word = "баран", description = "баран описание" });

        words.Add(new Word { id = 11, callNumber = 0, numberOfLetters = 4, word = "заяц", description = "заяц описание" });
        words.Add(new Word { id = 12, callNumber = 0, numberOfLetters = 5, word = "крыса", description = "крыса описание" });
        words.Add(new Word { id = 13, callNumber = 0, numberOfLetters = 6, word = "ехидна", description = "ехидна описание" });
        words.Add(new Word { id = 14, callNumber = 0, numberOfLetters = 6, word = "Свинья", description = "Савинья описание" });
        words.Add(new Word { id = 15, callNumber = 0, numberOfLetters = 6, word = "лисица", description = "лисица описание" });
        words.Add(new Word { id = 16, callNumber = 0, numberOfLetters = 5, word = "олень", description = "олень описание" });
        words.Add(new Word { id = 17, callNumber = 0, numberOfLetters = 6, word = "буйвол", description = "буйвол описание" });
        words.Add(new Word { id = 18, callNumber = 0, numberOfLetters = 8, word = "бурундук", description = "бурундук описание" });
        words.Add(new Word { id = 19, callNumber = 0, numberOfLetters = 4, word = "волк", description = "волк описание" });
        words.Add(new Word { id = 20, callNumber = 0, numberOfLetters = 6, word = "гепард", description = "гепард описание" });

        words.Add(new Word { id = 21, callNumber = 0, numberOfLetters = 3, word = "лев", description = "лев описание" });
        words.Add(new Word { id = 22, callNumber = 0, numberOfLetters = 3, word = "вол", description = "вол описание" });



    }




}
