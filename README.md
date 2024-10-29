# AstHelp
###### Программное средство управления комплектацией заказа при продаже товара

 [Макет дизайна Программного средства](https://www.figma.com/design/8CPJnddexH57UAMTKLGtJ4/%D0%B4%D0%B8%D0%BF%D0%BB%D0%BE%D0%BC?node-id=0-1&t=XpH2bcQ2ieBZyz36-1)

 [Class diadram](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/class.png)


В системе AstHelp реализован комплексный подход к управлению заказами и оборудованием, используя несколько ключевых веб-приложений и серверное приложение. Система предназначена для сотрудников Aston и включает в себя три основных веб-приложения: AstHelp Client, AstHelp Operator и AstHelp Picker. 

![C4-container](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/C4_container.jpg)
 

Серверное приложение AstHelp Backend реализует бизнес-логику и управляет взаимодействием между веб-приложениями и базой данных PostgreSQL. 

![C4-component](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/C4_component.jpg)


Архитектура системы основана на принципах DDD и CQRS. 

##  Архитектура

#### ERD
![ERD](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/ERD.png)

#### Диаграмма состояний

![state](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/state.png)

#### Диаграмма последовательности

![sequence](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/sequence.png)

#### Диаграмма деятельности

![activity](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/activity.png)

#### Алгоритм

![algoritm](https://github.com/polinaLesak/AstHelp/blob/main/doc/images/algoritm.jpg)

##  Пользовательский интерфейс

#### Первая страница

![1](https://github.com/polinaLesak/AstHelp/blob/main/doc/interface/1.jpg)

#### Каталог товаров

![1](https://github.com/polinaLesak/AstHelp/blob/main/doc/interface/katalog.jpg)


## Документация

Swagger находится в doc/api

## Оценка качества кода

Все значения метрик находятся на хорошем уровне и соответствуют принципам Clean Code. Они указывают на поддерживаемый, понятный и легко адаптируемый код.

Maintainability Index - 93.24
Cyclomatic Complexity - 2.75
Depth of Inheritance - 1.14
Class Coupling - 5.09
Lines of Source Code - 17.72
 Lines of Executable Code - 4.35 
