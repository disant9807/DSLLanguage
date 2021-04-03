# DSLLanguage
В проекте используется netcore5. 

### Семантическое ядро - реализовано в DSLCore/DSLSemanticModel/DSLSemanticController

### Построитель выражение - реализован в DSLCore/DSLBuilderExpression/DSLBuilder

### Использование DSL (Сама его реализация) представлена в DSLLanguage/Controllers/StaffController в action staffList and StaffCard

### Логика работы:
Сейчас DSL реализует лишь 2 компонента:
- Карточка элемента
- Список элементов.

###### Карточка элемента
Ее реализация через DSL сделана след образом:

    var builder = new ComponentCard(
                new Row(
                    new Column(
                        new ItemCardInfo(
                            text: staff?.StaffName ?? "Неизвестная профессия"
                        )
                    ),
                    new Column(
                        new ItemCardInfo(
                            text: "Дата добавления:" + staff?.Date.ToString("dd mm yyy") ?? "Неизвестная дата"
                        )
                    )
                ),
                new Row(
                    new Column(
                        new ItemCardInfo(
                            text: staff?.FIO ?? "Неизвестное имя"
                        )
                    ),
                    new Column(
                        new ItemCardInfo(
                            text: staff?.Competension ?? "Неизвестные компетенции"
                        )
                    )
                )
            );

Для ее создания используется класс `ComponentCard`, в конструктор которого передается массив `rows` and `columns` в `rows`. В `сolumn` добавляются объекты `itemCardInfo` для создания текстовых элементов внутри карточки

###### Список элементов
Ее реализация через DSL сделана след образом:
      
    var Builder = new ComponentList(
                StaffListDB.Staff.Select(
                    e => new Row(
                        new Column(
                            new ItemList(
                                title: e.StaffName,
                                text: e.FIO,
                                date: e.Date.ToString("dd mm yyy"),
                                smallText: e.Competension,
                                link: Url.RouteUrl(new { controller = "Staff", id = e.Id, action = "StaffCard" })
                            )
                        )
                    )
                ).ToArray()
            );
            
Для ее создания используется класс `ComponentList`, в конструктор которого передается массив `rows` and `columns` в `rows`. В `сolumn` добавляются объекты `ItemList` для создания элементов списка внутри списка
