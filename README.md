# ЗАДАНИЕ

## Меню ресторана

Продвинутый ресторатор вместо обычного печатного меню решил использовать планшет (например, iPad). Так как на один экран всё меню не помещается, то необходимо его разделить на страницы.

1. Необходимо реализовать объект MenuMaster, который работает с коллекцией блюд. Входные параметры:
 ▪️ Коллекция блюд.
 ▪️ Целое число - количество элементов на одной странице.

Методы MenuMaster, возвращают:
 ▪️ Общее количество блюд;
 ▪️ Количество страниц;
 ▪️ Количество блюд на указанной странице;
 ▪️ Блюда указанной страницы;
 ▪️ Список первых блюд каждой страницы.

Например, меню: {Матча, Латте, Смузи, Джин, Эскимо}, с двумя блюдами на странице, будет поделено на 3 страницы: {Матча, Латте}, {Смузи, Джин} и {Эскимо}.
 
2. Необходимо проверить все входные параметры на валидность. В случае неправильных входных параметров выкидывать исключения.

3. Класс MenuMaster должен быть покрыт тестами.
