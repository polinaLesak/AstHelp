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

### Тестирование

Модульные тесты обеспечивают высокую степень покрытия кода и стабильность отдельных компонентов, в то время как интеграционные тесты гарантируют, что вся система функционирует как единое целое. 

Модульные тесты:

``` namespace Identity.Microservice.Tests.Unit
{
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly LoginUserCommandHandler _handler;

        public LoginUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _tokenServiceMock = new Mock<ITokenService>();
            _handler = new LoginUserCommandHandler(_unitOfWorkMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
        {
            var command = new LoginUserCommand("testUser", "password");
            var user = new User { Username = "testUser", Password = BCrypt.Net.BCrypt.HashPassword("password"), IsActive = true };
            _unitOfWorkMock.Setup(uow => uow.Users.GetUserByUsernameAsync(command.Username)).ReturnsAsync(user);
            _tokenServiceMock.Setup(ts => ts.GenerateToken(user)).Returns("jwt_token");

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("jwt_token", result.JwtToken);
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenUserNotFound()
        {
            var command = new LoginUserCommand("invalidUser", "password");
            _unitOfWorkMock.Setup(uow => uow.Users.GetUserByUsernameAsync(command.Username)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
```

Интеграционные тесты:

```
namespace Identity.Microservice.Tests.Integration
{
    public class AuthIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            var registerDto = new RegisterUserCommand { Username = "testUser", Email = "test@example.com", Password = "password" };

            var response = await _client.PostAsJsonAsync("/api/auth/register", registerDto);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("true", result);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenLoginIsSuccessful()
        {
            var loginDto = new LoginUserCommand("testUser", "password");

            var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<UserLoginResponseDto>();
            Assert.NotNull(result.JwtToken);
        }
    }

}
```