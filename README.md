PUSHApplication
=====================
Выполнено
-----------------------------------
- Описание архитектуры разработанной системы
- Исходные тексты, пригодные для самостоятельной сборки 
- Инструкция по сборке исходных текстов

Предстоит выполнить
-----------------------------------
- Описание разработанных API, с примерами вызовов
- Пакет (или пакеты) для развертывания, в формате, подходящем для использования в Octopus Deploy
- Документация по развертыванию

Инструкция по сборке исходных текстов
-----------------------------------
Для успешного запуска приложение необходимо наличие установленного сервера MS SQL и Redis, а также очереди сообщений RebbitMq

Перед первым запуском приложения необходимо применить миграции, для этого нужно построить решение, 
затем выполнить миграции, выбрав стартовое приложение RegistrationService.Web, и Default project: PUSHApplication.DAL в Package Manager Console.
Далее в Package Manager Console необходимо выполнить команду Update-Database.

Описание API
-----------------------------------
1. RegistrationService.Web
   * RegistrationController.Registration([FromBody] MobileAppViewModel mobileAppViewModel) Регистрирует мобильное приложение. MobileAppViewModel mobileAppViewModel - Модель представления мобильного приложения
   * RegistrationController.UnRegistration(string token) Разрегистрация мобильного приложения по его токену. string token - Токен приложения
2. MessageReciever.Web
   * MessageController.Send([FromBody] MessageViewModel messageVm) Отправка сообщения в сервис обработки сообщений. MessageViewModel messageVm - Модель представления сообщения
3. MessageProcessing.Web
   * MessageProcessingController.Start() Запуск работы сервиса
4. FirabaseService.Web
   * FirebaseController.Start() Запуск работы сервиса
5. Statistic.Web
   * FirebaseController.GetRegisteredVersions() Возвращает список зарегестрированных версий приложений
   * FirebaseController.GetMessagesByPhoneNumber([FromQuery] PageParameters pageParameters, string phoneNumber) Получить список сообщений отправленных на указанный номер телефона. Поддержка пэйджинга. PageParameters pageParameters - параметры пэйджинга. string phoneNumber - номер телефона

Описание архитектуры
-----------------------------------
![Image alt](https://github.com/NikolayFenichev/PUSHApplication/raw/master/PUSHApplicationArhitecture.png)
