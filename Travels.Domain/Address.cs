﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Travels.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public CityEnum City { get; set; }
        public RegionEnum Region { get; set; }

        public ICollection<Attraction> Attractions { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegionEnum
    {
        Гомельская, // Гомельская область
        Витебская, // Витебская область
        Гродненская, // Гродненская область
        Минская, // Минская область
        Могилевская, // Могилевская область
        Брестская // Брестская область
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CityEnum
    {
        Гомель,
        Брагин,
        БудаКошелёво,
        Ветка,
        Добруш,
        Ельск,
        Житковичи,
        Жлобин,
        Калинковичи,
        Корма,
        Лельчицы,
        Лоев,
        Мозырь,
        Наровля,
        Октябрьский,
        Петриков,
        Речица,
        Рогачёв,
        Светлогорск,
        Хойники,
        Чечерск,

        Брест,
        Барановичи,
        Берёза,
        Ганцевичи,
        Дрогичин,
        Жабинка,
        Иваново,
        Ивацевичи,
        Каменец,
        Кобрин,
        Лунинец,
        Ляховичи,
        Малорита,
        Пинск,
        Пружаны,
        Столин,

        Витебск,
        Бешенковичи,
        Браслав,
        Верхнедвинск,
        Глубокое,
        Городок,
        Докшицы,
        Дубровно,
        Лепель,
        Лиозно,
        Миоры,
        Орша,
        Полоцк,
        Поставы,
        Россоны,
        Сенно,
        Толочин,
        Ушачи,
        Чашники,
        Шарковщина,
        Шумилино,

        Гродно,
        Берестовица,
        Волковыск,
        Вороново,
        Дятлово,
        Зельва,
        Ивье,
        Кореличи,
        Лида,
        Мосты,
        Новогрудок,
        Ошмяны,
        Островец,
        Свислочь,
        Слоним,
        Сморгонь,
        Щучин,

        Минск,
        Березино,
        Борисов,
        Вилейка,
        Воложин,
        Дзержинск,
        Клецк,
        Копыль,
        Крупки,
        Логойск,
        Любань,
        Молодечно,
        Мядель,
        Несвиж,
        Пуховичи,
        Слуцк,
        Смолевичи,
        Солигорск,
        СтарыеДороги,
        Столбцы,
        Узда,
        Червень,

        Могилёв,
        Белыничы,
        Бобруйск,
        Быхов,
        Глуск,
        Горки,
        Дрибин,
        Кировск,
        Климовичи,
        Кличев,
        Краснополье,
        Кричев,
        Круглое,
        Костюковичи,
        Мстиславль,
        Осиповичи,
        Славгород,
        Хотимск,
        Чаусы,
        Чериков,
        Шклов
    }
}
