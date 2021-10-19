# DSS "Альтернатива"
Программка поддержки принятия решений на основе метода анализа иерархий (сокращенно МАИ). Одностраничное веб-приложение на Blazor WebAssembly, а ещё некогда дипломный проект (ВКР).

**Что делает?**

Приложение позволяет быстро применить МАИ для поиска наилучшей альтернативы без знаний теории принятия решений и изучения систем поддержки принятия решений.
Воплощению концепции способствует и веб-формат, и большая часть дизайнерских решений.

**Ссылка**

https://alleaxx.github.io/DssAlternative/

## Технологии
*C# / Blazor WebAssembly*

Для визуализации метода анализа иерархий необходим интерактивный интерфейс, а для реализации программной логики его работы - солидный язык, не взрывающий мозг. Мозг пусть взрывает алгоритм многоуровневого МАИ… 
С учетом веб-формата, желания опробовать новые технологии и не разделять сложную логику на два языка - выбор был очевиден.

Впрочем, существует и прототип этого же приложения в качестве – [десктопной WPF-программы](https://github.com/Alleaxx/DSS/tree/master/DSSView). Именно на её основе была сделана эта системка.

## Использование

Сперва, разумеется, стоит перейти в [само приложение](https://alleaxx.github.io/DssAlternative/)

![Иерархия проблемы](https://i.ibb.co/PmhnSZZ/image.png)

Ну а далее секреты работы системы можно познать сразу несколькими способами:
	1. Прочитать раздел в самом приложении об общих принципах системы
	2. Загрузить предложенный пример и изучить всё самому

## Возможности
С помощью проекта можно выявить предпочтения пользователя в вариантах решения возникшей проблемы выбора, определить наиболее подходящее решение и математически обосновать. Полученные результаты могут служить увесистым аргументом для окончательного выбора той или иной альтернативы.

**Для этого приложение:**
- Реализует алгоритм МАИ с поддержкой многоуровневой иерархии
- Визуализирует этапы решений задач согласно МАИ:
    - Интерактивная схема иерархии
    - Заполнений отношений с помощью словесных оценок
    - Помощь в согласовании оценок
    - Анализ результата
- Предоставляет шаблоны для решения распространенных задач
- Позволяет решать сразу несколько задач параллельно
- Сохраняет прогресс решения и пользовательские задачи по необходимости

### Какие задачи можно решать?
Метод анализа иерархий можно применять в самых разных ситуациях - от житейских выборов и вопросов до полноценных исследований.
- Выбор между несколькими местами учебы / работы
- Определение оптимального выбора при покупке чего-либо
-  ...

![Формирование связей](https://i.ibb.co/7RykYqQ/image.png)

Теоретические возможности приложения простираются и до таких пределов как:
- Обоснование стека технологий проекта
- Выбор кандидата в команду проекта
-  …

Но, правда, только теоретические. Серьезным намерениям - серьезный инструмент.

## Подробности
Одной из ключевых особенностей работы является формирование иерархий за счет группировки списка узлов, работа со связями этих узлов друг с другом, а также формирование матриц на основе этих отношений.

Сохранение задач и прогресса происходит в JSON-формате. Прогресс решения задач и сохраненные пользовательские шаблоны хранятся в браузере при помощи локального хранилища.

Ещё более подробно приложение описано в выпускной квалификационной работе. Ну, той самой, где еще 130 страниц! Надеюсь, после после её прочтения вопросов не появится.

## Планы на будущее
- Полноценная интерактивная схема иерархии, поддержка редактирования многоуровневых иерархий
    - История действий в редакторе
- Система автоисправления рассогласованности пользовательских оценок
- Рефакторинг кода и стилей
- Поддержка аккаунтов пользователей
- Возможности оформить обоснования решения задачи в отчет (файл)
- Добавление возможности сохранения файлов