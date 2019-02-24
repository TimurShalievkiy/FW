using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}


[Serializable]
public class JsonData
{
    //скорость ракетки
    public int rocketSpeed;

    //скорость шайбы
    public int ballSpeed;

    //скорсть кластера
    public float clasterSpeed;

    public JsonData(int rocketSpeed, int ballSpeed, float clasterSpeed)
    {
        this.rocketSpeed = rocketSpeed;
        this.ballSpeed = ballSpeed;
        this.clasterSpeed = clasterSpeed;
    }

    //public JsonData() { }
}

public class DataManager : MonoBehaviour
{
    //сериализуемый обьект 
    public static JsonData data;


    public void Start()
    {
        //базовая инициализация
        //data = new JsonData(12, 10,0.5f);

        //получение данных с json
        TextAsset file = Resources.Load("gameData") as TextAsset;
        string content = file.ToString();


        data = JsonUtility.FromJson<JsonData>(content);
    }

    public static void SaveTheme()
    {
        Word w = new Word(1, 0, 2, "Уж", "");
        Word[] listWords = new Word[]
        {
        new Word (  1, 0, 2,  "Уж",    ""),
        new Word (  2, 0, 2,  "Ёж",    ""),
        new Word (  3, 0, 3,  "Ёрш",    ""),
        new Word (  4, 0, 3,  "Лев",    ""),
        new Word (  5, 0, 3,  "Тур",    ""),
        new Word (  6, 0, 3,  "Рак",    ""),
        new Word (  7, 0, 3,  "Жук",    ""),
        new Word (  8, 0, 3,  "Оса",    ""),
        new Word (  9, 0, 3,  "Сыч",    ""),
        new Word (  10  , 0, 3,  "Тля",    ""),
        new Word (  11  , 0, 4,  "Гриф",    ""),
        new Word (  12  , 0, 4,  "Енот",    ""),
        new Word (  13  , 0, 4,  "Жаба",    ""),
new Word (  14  , 0, 4,  "Змея",    ""),
new Word (  15  , 0, 4,  "Киви",    ""),
new Word (  16  , 0, 4,  "Краб",    ""),
new Word (  17  , 0, 4,  "Лось",    ""),
new Word (  18  , 0, 4,  "Моль",    ""),
new Word (  19  , 0, 4,  "Овод",    ""),
new Word (  20  , 0, 4,  "Орёл",    ""),
new Word (  21  , 0, 4,  "Осёл",    ""),
new Word (  22  , 0, 4,  "Рысь",    ""),
new Word (  23  , 0, 4,  "Слон",    ""),
new Word (  24  , 0, 4,  "Удав",    ""),
new Word (  25  , 0, 4,  "Усач",    ""),
new Word (  26  , 0, 4,  "Волк",    ""),
new Word (  27  , 0, 4,  "Гага",    ""),
new Word (  28  , 0, 4,  "Заяц",    ""),
new Word (  29  , 0, 4,  "Ибис",    ""),
new Word (  30  , 0, 4,  "Клещ",    ""),
new Word (  31  , 0, 4,  "Лань",    ""),
new Word (  32  , 0, 4,  "Муха",    ""),
new Word (  33  , 0, 4,  "Ткач",    ""),
new Word (  34  , 0, 4,  "Хрущ",    ""),
new Word (  35  , 0, 4,  "Ящер",    ""),
new Word (  36  , 0, 4,  "Бобр",    ""),
new Word (  37  , 0, 4,  "Зубр",    ""),
new Word (  38  , 0, 4,  "Крот",    ""),
new Word (  39  , 0, 4,  "Лама",    ""),
new Word (  40  , 0, 4,  "Мышь",    ""),
new Word (  41  , 0, 4,  "Овца",    ""),
new Word (  42  , 0, 4,  "Паук",    ""),
new Word (  43  , 0, 4,  "Скат",    ""),
new Word (  44  , 0, 4,  "Сова",    ""),
new Word (  45  , 0, 4,  "Тигр",    ""),
new Word (  46  , 0, 5,  "Бизон",    ""),
new Word (  47  , 0, 5,  "Варан",    ""),
new Word (  48  , 0, 5,  "Выдра",    ""),
new Word (  49  , 0, 5,  "Дрофа",    ""),
new Word (  50  , 0, 5,  "Жираф",    ""),
new Word (  51  , 0, 5,  "Зебра",    ""),
new Word (  52  , 0, 5,  "Кабан",    ""),
new Word (  53  , 0, 5,  "Кобра",    ""),
new Word (  54  , 0, 5,  "Крыса",    ""),
new Word (  55  , 0, 5,  "Окунь",    ""),
new Word (  56  , 0, 5,  "Судак",    ""),
new Word (  57  , 0, 5,  "Хомяк",    ""),
new Word (  58  , 0, 5,  "Шмель",    ""),
new Word (  59  , 0, 5,  "Кулик",    ""),
new Word (  60  , 0, 5,  "Акула",    ""),
new Word (  61  , 0, 5,  "Баран",    ""),
new Word (  62  , 0, 5,  "Ворон",    ""),
new Word (  63  , 0, 5,  "Губка",    ""),
new Word (  64  , 0, 5,  "Дятел",    ""),
new Word (  65  , 0, 5,  "Козёл",    ""),
new Word (  66  , 0, 5,  "Конёк",    ""),
new Word (  67  , 0, 5,  "Лемур",    ""),
new Word (  68  , 0, 5,  "Мамба",    ""),
new Word (  69  , 0, 5,  "Осётр",    ""),
new Word (  70  , 0, 5,  "Панда",    ""),
new Word (  71  , 0, 5,  "Питон",    ""),
new Word (  72  , 0, 5,  "Сойка",    ""),
new Word (  73  , 0, 5,  "Тунец",    ""),
new Word (  74  , 0, 5,  "Цапля",    ""),
new Word (  75  , 0, 5,  "Шпрот",    ""),
new Word (  76  , 0, 5,  "Щучка",    ""),
new Word (  77  , 0, 5,  "Ягуар",    ""),
new Word (  78  , 0, 5,  "Белка",    ""),
new Word (  79  , 0, 5,  "Вобла",    ""),
new Word (  80  , 0, 5,  "Дрозд",    ""),
new Word (  81  , 0, 5,  "Комар",    ""),
new Word (  82  , 0, 5,  "Кошка",    ""),
new Word (  83  , 0, 5,  "Нанду",    ""),
new Word (  84  , 0, 5,  "Норка",    ""),
new Word (  85  , 0, 5,  "Олень",    ""),
new Word (  86  , 0, 5,  "Полоз",    ""),
new Word (  87  , 0, 5,  "Пчела",    ""),
new Word (  88  , 0, 5,  "Сокол",    ""),
new Word (  89  , 0, 5,  "Стриж",    ""),
new Word (  90  , 0, 5,  "Сурок",    ""),
new Word (  91  , 0, 5,  "Филин",    ""),
new Word (  92  , 0, 5,  "Хорёк",    ""),
new Word (  93  , 0, 5,  "Чайка",    ""),
new Word (  94  , 0, 6,  "Анчоус",    ""),
new Word (  95  , 0, 6,  "Барсук",    ""),
new Word (  96  , 0, 6,  "Геккон",    ""),
new Word (  97  , 0, 6,  "Гиббон",    ""),
new Word (  98  , 0, 6,  "Иволга",    ""),
new Word (  99  , 0, 6,  "Игуана",    ""),
new Word (  100 , 0, 6,  "Кайман",    ""),
new Word (  101 , 0, 6,  "Какаду",    ""),
new Word (  102 , 0, 6,  "Кондор",    ""),
new Word (  103 , 0, 6,  "Коршун",    ""),
new Word (  104 , 0, 6,  "Косуля",    ""),
new Word (  105 , 0, 6,  "Кролик",    ""),
new Word (  106 , 0, 6,  "Муфлон",    ""),
new Word (  107 , 0, 6,  "Страус",    ""),
new Word (  108 , 0, 6,  "Суслик",    ""),
new Word (  109 , 0, 6,  "Тюлень",    ""),
new Word (  110 , 0, 6,  "Ястреб",    ""),
new Word (  111 , 0, 6,  "Буйвол",    ""),
new Word (  112 , 0, 6,  "Гадюка",    ""),
new Word (  113 , 0, 6,  "Гарпия",    ""),
new Word (  114 , 0, 6,  "Корова",    ""),
new Word (  115 , 0, 6,  "Крылан",    ""),
new Word (  116 , 0, 6,  "Куница",    ""),
new Word (  117 , 0, 6,  "Лебедь",    ""),
new Word (  118 , 0, 6,  "Летяга",    ""),
new Word (  119 , 0, 6,  "Лисица",    ""),
new Word (  120 , 0, 6,  "Сайгак",    ""),
new Word (  121 , 0, 6,  "Синица",    ""),
new Word (  122 , 0, 6,  "Собака",    ""),
new Word (  123 , 0, 6,  "Тюлька",    ""),
new Word (  124 , 0, 6,  "Улитка",    ""),
new Word (  125 , 0, 6,  "Гадюка",    ""),
new Word (  126 , 0, 6,  "Газель",    ""),
new Word (  127 , 0, 6,  "Гепард",    ""),
new Word (  128 , 0, 6,  "Голубь",    ""),
new Word (  129 , 0, 6,  "Ехидна",    ""),
new Word (  130 , 0, 6,  "Карась",    ""),
new Word (  131 , 0, 6,  "Катран",    ""),
new Word (  132 , 0, 6,  "Курица",    ""),
new Word (  133 , 0, 6,  "Лошадь",    ""),
new Word (  134 , 0, 6,  "Мурена",    ""),
new Word (  135 , 0, 6,  "Мухоед",    ""),
new Word (  136 , 0, 6,  "Павиан",    ""),
new Word (  137 , 0, 6,  "Сорока",    ""),
new Word (  138 , 0, 6,  "Кролик",    ""),
new Word (  139 , 0, 7,  "Бегемот",    ""),
new Word (  140 , 0, 7,  "Воробей",    ""),
new Word (  141 , 0, 7,  "Журавль",    ""),
new Word (  142 , 0, 7,  "Кукушка",    ""),
new Word (  143 , 0, 7,  "Лангуст",    ""),
new Word (  144 , 0, 7,  "Ленивец",    ""),
new Word (  145 , 0, 7,  "Носорог",    ""),
new Word (  146 , 0, 7,  "Саранча",    ""),
new Word (  147 , 0, 7,  "Скворец",    ""),
new Word (  148 , 0, 7,  "Утконос",    ""),
new Word (  149 , 0, 7,  "Дельфин",    ""),
new Word (  150 , 0, 7,  "Бабочка",    ""),
new Word (  151 , 0, 7,  "Горилла",    ""),
new Word (  152 , 0, 7,  "Дельфин",    ""),
new Word (  153 , 0, 7,  "Кальмар",    ""),
new Word (  154 , 0, 7,  "Кенгуру",    ""),
new Word (  155 , 0, 7,  "Колибри",    ""),
new Word (  156 , 0, 7,  "Лемминг",    ""),
new Word (  157 , 0, 7,  "Леопард",    ""),
new Word (  158 , 0, 7,  "Мангуст",    ""),
new Word (  159 , 0, 7,  "Муравей",    ""),
new Word (  160 , 0, 7,  "Опоссум",    ""),
new Word (  161 , 0, 7,  "Пингвин",    ""),
new Word (  162 , 0, 7,  "Полёвка",    ""),
new Word (  163 , 0, 7,  "Попугай",    ""),
new Word (  164 , 0, 7,  "Сверчок",    ""),
new Word (  165 , 0, 7,  "Соловей",    ""),
new Word (  166 , 0, 7,  "Верблюд",    ""),
new Word (  167 , 0, 7,  "Глухарь",    ""),
new Word (  168 , 0, 7,  "Горлица",    ""),
new Word (  169 , 0, 7,  "Индейка",    ""),
new Word (  170 , 0, 7,  "Косатка",    ""),
new Word (  171 , 0, 7,  "Лягушка",    ""),
new Word (  172 , 0, 7,  "Пеликан",    ""),
new Word (  173 , 0, 7,  "Перепел",    ""),
new Word (  174 , 0, 7,  "Слизень",    ""),
new Word (  175 , 0, 7,  "Снегирь",    ""),
new Word (  176 , 0, 7,  "Таракан",    ""),
new Word (  177 , 0, 7,  "Тетерев",    ""),
new Word (  178 , 0, 7,  "Устрица",    ""),
new Word (  179 , 0, 7,  "Ящерица",    ""),
new Word (  180 , 0, 8,  "Дикобраз",    ""),
new Word (  181 , 0, 8,  "Древолаз",    ""),
new Word (  182 , 0, 8,  "Антилопа",    ""),
new Word (  183 , 0, 8,  "Долгопят",    ""),
new Word (  184 , 0, 8,  "Стрекоза",    ""),
new Word (  185 , 0, 8,  "Султанка",    ""),
new Word (  186 , 0, 8,  "Веслоног",    ""),
new Word (  187 , 0, 8,  "Крокодил",    ""),
new Word (  188 , 0, 8,  "Листонос",    ""),
new Word (  189 , 0, 8,  "Мартышка",    ""),
new Word (  190 , 0, 8,  "Нетопырь",    ""),
new Word (  191 , 0, 8,  "Обезьяна",    ""),
new Word (  192 , 0, 8,  "Скорпион",    ""),
new Word (  193 , 0, 8,  "Странник",    ""),
new Word (  194 , 0, 8,  "Удильщик",    ""),
new Word (  195 , 0, 8,  "Черепаха",    ""),
new Word (  196 , 0, 8,  "Шишечник",    ""),
new Word (  197 , 0, 9,  "Безглазка",    ""),
new Word (  198 , 0, 9,  "Белозубка",    ""),
new Word (  199 , 0, 9,  "Верблюдка",    ""),
new Word (  200 , 0, 9,  "Водомерка",    ""),
new Word (  201 , 0, 9,  "Звездочёт",    ""),
new Word (  202 , 0, 9,  "Куропатка",    ""),
new Word (  203 , 0, 9,  "Ленточник",    ""),
new Word (  204 , 0, 9,  "Мухоловка",    ""),
new Word (  205 , 0, 9,  "Пестрянка",    ""),
new Word (  206 , 0, 9,  "Шелкопряд",    ""),
new Word (  207 , 0, 9,  "Шипохвост",    ""),
new Word (  208 , 0, 9,  "Барракуда",    ""),
new Word (  209 , 0, 9,  "Блестянка",    ""),
new Word (  210 , 0, 9,  "Вилохвост",    ""),
new Word (  211 , 0, 9,  "Камышовка",    ""),
new Word (  212 , 0, 9,  "Жаворонок",    ""),
new Word (  213 , 0, 9,  "Зеленушка",    ""),
new Word (  214 , 0, 9,  "Иглохвост",    ""),
new Word (  215 , 0, 9,  "Каменушка",    ""),
new Word (  216 , 0, 9,  "Крапивник",    ""),
new Word (  217 , 0, 9,  "Ланцетник",    ""),
new Word (  218 , 0, 9,  "Малиновка",    ""),
new Word (  219 , 0, 9,  "Орангутан",    ""),
new Word (  220 , 0, 9,  "Пеструшка",    ""),
new Word (  221 , 0, 9,  "Пилильщик",    ""),
new Word (  222 , 0, 9,  "Радужница",    ""),
new Word (  223 , 0, 9,  "Щитоноска",    ""),
new Word (  224 , 0, 10  ,  "Сардинелла",    ""),
new Word (  225 , 0, 10  ,  "Белокровка",    ""),
new Word (  226 , 0, 10  ,  "Броненосец",    ""),
new Word (  227 , 0, 10  ,  "Двухвостка",    ""),
new Word (  228 , 0, 10  ,  "Желтопузик",    ""),
new Word (  229 , 0, 10  ,  "Каракатица",    ""),
new Word (  230 , 0, 10  ,  "Моллюскоед",    ""),
new Word (  231 , 0, 10  ,  "Веретеница",    ""),
new Word (  232 , 0, 10  ,  "Вилохвостк",    ""),
new Word (  233 , 0, 10  ,  "Древесница",    ""),
new Word (  234 , 0, 10  ,  "Ленточница",    ""),
new Word (  235 , 0, 10  ,  "Нектарница",    ""),
new Word (  236 , 0, 10  ,  "Саламандра",    ""),
new Word (  237 , 0, 10  ,  "Тростнянка",    ""),
new Word (  238 , 0, 10  ,  "Трясогузка",    ""),
new Word (  239 , 0, 11  ,  "Желтоглазка",    ""),
new Word (  240 , 0, 11  ,  "Ложноглазка",    ""),
new Word (  241 , 0, 11  ,  "Медоуказчик",    ""),
new Word (  242 , 0, 11  ,  "Плоскохвост",    ""),
new Word (  243 , 0, 11  ,  "Сколопендра",    ""),
new Word (  244 , 0, 11  ,  "Трёхпёрстка",    ""),
new Word (  245 , 0, 11  ,  "Широколобка",    ""),
new Word (  246 , 0, 11  ,  "Щитомордник",    ""),
new Word (  247 , 0, 11  ,  "Буревестник",    ""),
new Word (  248 , 0, 11  ,  "Веерокрылка",    ""),
new Word (  249 , 0, 11  ,  "Вислокрылка",    ""),
new Word (  250 , 0, 11  ,  "Змееящерица",    ""),
new Word (  251 , 0, 11  ,  "Пересмешник",    ""),
    new Word (  252 , 0, 11  ,  "Скрытохвост",    ""),
    new Word (  253 , 0, 11  ,  "Широконоска",    ""),
    new Word (  254 , 0, 12  ,  "Перепелятник",    ""),
    new Word (  255 , 0, 12  ,  "Веерохвостка",    "")
        };

        string str = JsonHelper.ToJson(listWords, true);

        Debug.Log(str);


    }
}


