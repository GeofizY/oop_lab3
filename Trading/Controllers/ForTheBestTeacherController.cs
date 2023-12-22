using Microsoft.AspNetCore.Mvc;

namespace Trading.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForTheBestTeacherController : ControllerBase
{

    [HttpGet]
    public string HappyBirthday()
    {
        return "Поздравляю Вас С Днем Рождения!!! Желаю всего самого наилучшего и, чтобы никакие невзгоды не портили Ваше настроение (ну например ИТМО, но я этого Вам не говорил :D)";
    }
}