# Система прогнозирования цен на товары в магазинах
На основе истории цен на товары в магазинах прогнозировать их стоимость в следующем месяце.

## Описание предметной области
Имеется база данных, хранящая информацию о магазинах, их ассортименте товаров с историей о цене товара не более чем за последние полтора года и покупателях. На основе истории цены товара строится тренд, с помощью которой можно предсказать цену товара на следующий месяц.

## Анализ аналогичный решений

Продукт | Учет исторических данных | Открытость | Разграничение прав доступа
:-------|:------------------------:|:----------:|:-------------------------:
Novo Forecast |+|-|+
GoodsForecast.Planning |+|-|-

## Целесообразность и актуальность
Проект имеет минимальный функционал для прогнозирования цен на товары, разграничение прав доступа для разных типов пользователей и распространяется бесплатно с открытым исходным кодом

## Use-Case - диаграмма
![](/docs/imgs/use_case.drawio.png)

## ER-диаграмма сущностей
![](/docs/imgs/ER.drawio.png)

## Архитектурные характеристики
Архитектура проекта основана на паттерне MVVM

## Описание типа приложения и технологического стека
* Тип приложения: Desktop WPF (.Net 6)
* Технологический стек: C# 10, PostgreSQL, NpgSQL, Entity Framework Core 6

## Диаграмма компонентов
![](/docs/imgs/HighComponents.drawio.png)

## UML диаграмма классов для компонента доступа к данным и компонента с бизнес-логикой
![](/docs/imgs/DABL.png)

## UML диаграммы «модельных» классов сущностей

### Сущности базы данных
![](/docs/imgs/ModelClassesDB.png)

### Сущности системы
![](/docs/imgs/SysClasses.png)

### Транспортные сущности
![](/docs/imgs/SysClasses.png)
=======
Архитектура проекта основана на паттерне MVVM.
