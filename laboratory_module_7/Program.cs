/* Курс: Шаблоны проектирования приложений

Тема: Модуль 07 Паттерны поведения. Команда. Шаблонный метод. Посредник

Цель:
Изучить и реализовать паттерн Команда (Command) на языке C#. Ваша задача — создать систему управления умным домом, где различные устройства могут управляться через команды, отправляемые с пульта дистанционного управления. Каждый тип команды должен быть инкапсулирован в отдельный класс.
Описание задачи:
Необходимо реализовать систему управления умным домом, которая позволяет контролировать различные устройства (например, свет, телевизор, кондиционер) через команды. Каждое устройство должно поддерживать выполнение команд включения, выключения и, возможно, других действий. Система должна быть гибкой, чтобы легко добавлять новые команды и устройства.
Структура программы:
1.	Интерфейс ICommand — описывает контракт для всех команд.
2.	Классы конкретных команд — реализуют различные действия для устройств (включение, выключение и т.д.).
3.	Класс устройства — представляет конкретные устройства (например, свет, телевизор и т.д.).
4.	Класс пульта дистанционного управления — хранит команды и позволяет выполнять их.
5.	Клиентский код — демонстрирует работу системы управления умным домом.
Шаги выполнения:
1. Создайте интерфейс ICommand, который будет содержать метод для выполнения команды

public interface ICommand
{
    void Execute();
    void Undo(); // Метод для отмены команды
}

2. Реализуйте классы команд для конкретных действий. Например, команды для включения и выключения света.
Команда для включения света:

public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }

    public void Undo()
    {
        _light.Off();
    }
}

Команда для выключения света:
public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.Off();
    }

    public void Undo()
    {
        _light.On();
    }
}

3. Создайте класс устройства, например, Light:
public class Light
{
    public void On()
    {
        Console.WriteLine("Свет включен.");
    }

    public void Off()
    {
        Console.WriteLine("Свет выключен.");
    }
}

4. Реализуйте другие устройства, такие как Television и AirConditioner, аналогично классу Light. Пример устройства Television:

public class Television
{
    public void On()
    {
        Console.WriteLine("Телевизор включен.");
    }

    public void Off()
    {
        Console.WriteLine("Телевизор выключен.");
    }
}

5. Реализуйте класс пульта дистанционного управления, который будет принимать команды и их выполнять:

public class RemoteControl
{
    private ICommand _onCommand;
    private ICommand _offCommand;

    public void SetCommands(ICommand onCommand, ICommand offCommand)
    {
        _onCommand = onCommand;
        _offCommand = offCommand;
    }

    public void PressOnButton()
    {
        _onCommand.Execute();
    }

    public void PressOffButton()
    {
        _offCommand.Execute();
    }

    public void PressUndoButton()
    {
        _onCommand.Undo();
    }
}

6. Напишите клиентский код для демонстрации работы пульта с командами:

class Program
{
    static void Main(string[] args)
    {
        // Создаем устройства
        Light livingRoomLight = new Light();
        Television tv = new Television();

        // Создаем команды для управления светом
        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);

        // Создаем команды для управления телевизором
        ICommand tvOn = new TelevisionOnCommand(tv);
        ICommand tvOff = new TelevisionOffCommand(tv);

        // Создаем пульт и привязываем команды к кнопкам
        RemoteControl remote = new RemoteControl();
        
        // Управляем светом
        remote.SetCommands(lightOn, lightOff);
        Console.WriteLine("Управление светом:");
        remote.PressOnButton();
        remote.PressOffButton();
        remote.PressUndoButton();

        // Управляем телевизором
        remote.SetCommands(tvOn, tvOff);
        Console.WriteLine("\nУправление телевизором:");
        remote.PressOnButton();
        remote.PressOffButton();
    }
}

Задания:
1.	Реализуйте код по шагам выше.
2.	Тестирование:
o	Проверьте работу команд для включения и выключения света.
o	Проверьте работу команд для включения и выключения телевизора.
o	Проверьте работу кнопки отмены действия (Undo).
3.	Расширение функционала:
o	Добавьте новые устройства (например, кондиционер, аудиосистему) и реализуйте команды для их управления.
o	Реализуйте макрокоманды (MacroCommand), которые будут выполнять несколько команд подряд.
o	Добавьте возможность переключения на различные режимы работы (например, режим экономии энергии для кондиционера).
4.	Обработка ошибок:
o	Добавьте обработку ситуации, когда на кнопке пульта не назначена команда (например, вывод сообщения об ошибке).
o	Добавьте логирование всех выполненных команд для отслеживания действий пользователя.
Вопросы для самопроверки:
1.	В чем преимущество использования паттерна "Команда" для управления устройствами?
2.	Как вы можете расширить функционал пульта, добавив новые кнопки для других устройств, не изменяя существующий код?
3.	В чем разница между командами и макрокомандами, и как они помогают улучшить гибкость системы? */
/* using System;
using System.Collections.Generic;

namespace SmartHomeCommand
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class NoCommand : ICommand
    {
        public void Execute() => Console.WriteLine("[WARN] Команда не назначена");
        public void Undo() => Console.WriteLine("[WARN] Отменять нечего");
    }

    public class Light
    {
        public void On() => Console.WriteLine("Свет включен");
        public void Off() => Console.WriteLine("Свет выключен");
    }

    public class Television
    {
        public void On() => Console.WriteLine("Телевизор включен");
        public void Off() => Console.WriteLine("Телевизор выключен");
    }

    public class AirConditioner
    {
        private bool eco = false;
        public void On() => Console.WriteLine("Кондиционер включен");
        public void Off() => Console.WriteLine("Кондиционер выключен");
        public void EcoOn() { eco = true; Console.WriteLine("Кондиционер: режим ЭКО включен"); }
        public void EcoOff() { eco = false; Console.WriteLine("Кондиционер: режим ЭКО выключен"); }
    }

    public class LightOnCommand : ICommand
    {
        private readonly Light l;
        public LightOnCommand(Light l) => this.l = l;
        public void Execute() => l.On();
        public void Undo() => l.Off();
    }

    public class LightOffCommand : ICommand
    {
        private readonly Light l;
        public LightOffCommand(Light l) => this.l = l;
        public void Execute() => l.Off();
        public void Undo() => l.On();
    }

    public class TelevisionOnCommand : ICommand
    {
        private readonly Television t;
        public TelevisionOnCommand(Television t) => this.t = t;
        public void Execute() => t.On();
        public void Undo() => t.Off();
    }

    public class TelevisionOffCommand : ICommand
    {
        private readonly Television t;
        public TelevisionOffCommand(Television t) => this.t = t;
        public void Execute() => t.Off();
        public void Undo() => t.On();
    }

    public class AirOnCommand : ICommand
    {
        private readonly AirConditioner a;
        public AirOnCommand(AirConditioner a) => this.a = a;
        public void Execute() => a.On();
        public void Undo() => a.Off();
    }

    public class AirOffCommand : ICommand
    {
        private readonly AirConditioner a;
        public AirOffCommand(AirConditioner a) => this.a = a;
        public void Execute() => a.Off();
        public void Undo() => a.On();
    }

    public class AirEcoOnCommand : ICommand
    {
        private readonly AirConditioner a;
        public AirEcoOnCommand(AirConditioner a) => this.a = a;
        public void Execute() => a.EcoOn();
        public void Undo() => a.EcoOff();
    }

    public class MacroCommand : ICommand
    {
        private readonly ICommand[] cmds;
        public MacroCommand(params ICommand[] cmds) => this.cmds = cmds ?? Array.Empty<ICommand>();
        public void Execute() { foreach (var c in cmds) c.Execute(); }
        public void Undo() { for (int i = cmds.Length - 1; i >= 0; i--) cmds[i].Undo(); }
    }

    public class RemoteControl
    {
        private ICommand on = new NoCommand();
        private ICommand off = new NoCommand();
        private readonly Stack<ICommand> history = new Stack<ICommand>();
        private readonly List<string> log = new List<string>();

        public void SetCommands(ICommand onCommand, ICommand offCommand)
        {
            on = onCommand ?? new NoCommand();
            off = offCommand ?? new NoCommand();
            Log($"Назначены команды: {on.GetType().Name} / {off.GetType().Name}");
        }

        public void PressOnButton()
        {
            on.Execute();
            history.Push(on);
            Log($"Выполнено: {on.GetType().Name}");
        }

        public void PressOffButton()
        {
            off.Execute();
            history.Push(off);
            Log($"Выполнено: {off.GetType().Name}");
        }

        public void PressUndoButton()
        {
            if (history.Count == 0) { Console.WriteLine("[WARN] История пуста"); return; }
            var cmd = history.Pop();
            cmd.Undo();
            Log($"Отменено: {cmd.GetType().Name}");
        }

        public void RunMacro(ICommand macro)
        {
            var m = macro ?? new NoCommand();
            m.Execute();
            history.Push(m);
            Log($"Выполнено: {m.GetType().Name}");
        }

        public void PrintLog()
        {
            Console.WriteLine("Лог команд:");
            foreach (var s in log) Console.WriteLine(s);
        }

        private void Log(string s) => log.Add($"[{DateTime.Now:HH:mm:ss}] {s}");
    }

    class Program
    {
        static void Main()
        {
            var remote = new RemoteControl();

            var light = new Light();
            var tv = new Television();
            var air = new AirConditioner();

            var lightOn = new LightOnCommand(light);
            var lightOff = new LightOffCommand(light);

            var tvOn = new TelevisionOnCommand(tv);
            var tvOff = new TelevisionOffCommand(tv);

            var airOn = new AirOnCommand(air);
            var airOff = new AirOffCommand(air);
            var airEco = new AirEcoOnCommand(air);

            Console.WriteLine("Управление светом:");
            remote.SetCommands(lightOn, lightOff);
            remote.PressOnButton();
            remote.PressOffButton();
            remote.PressUndoButton();

            Console.WriteLine();
            Console.WriteLine("Управление телевизором:");
            remote.SetCommands(tvOn, tvOff);
            remote.PressOnButton();
            remote.PressOffButton();
            remote.PressUndoButton();

            Console.WriteLine();
            Console.WriteLine("Управление кондиционером:");
            remote.SetCommands(airOn, airOff);
            remote.PressOnButton();
            remote.PressOffButton();
            remote.PressUndoButton();

            Console.WriteLine();
            Console.WriteLine("Режим ЭКО для кондиционера:");
            remote.SetCommands(airEco, airOff);
            remote.PressOnButton();
            remote.PressUndoButton();

            Console.WriteLine();
            Console.WriteLine("Макрокоманда: вечерний сценарий (Дархан)");
            var evening = new MacroCommand(lightOn, tvOn, airOn, airEco);
            remote.RunMacro(evening);
            remote.PressUndoButton();

            Console.WriteLine();
            Console.WriteLine("Проверка пустой кнопки:");
            remote.SetCommands(null, null);
            remote.PressOnButton();
            remote.PressOffButton();
            remote.PressUndoButton();

            Console.WriteLine();
            remote.PrintLog();
        }
    }
} */


/* Цель:
Изучить и реализовать паттерн Шаблонный метод (Template Method) на языке C#. Ваша задача — создать систему для приготовления различных напитков (например, чай и кофе), где общий алгоритм приготовления будет описан в базовом классе, а конкретные шаги будут определяться в наследниках.
Описание задачи:
Необходимо реализовать систему для приготовления напитков, таких как чай и кофе. Общий процесс приготовления напитков включает несколько шагов (например, кипячение воды, добавление ингредиентов и т.д.). Паттерн "Шаблонный метод" используется для того, чтобы общий алгоритм был определён в базовом классе, а конкретные шаги (например, добавление чая или кофе) реализовались в подклассах.
Структура программы:
1.	Базовый класс — определяет общий шаблонный метод приготовления напитка, включающий конкретные и абстрактные шаги.
2.	Классы-наследники — реализуют конкретные шаги для приготовления различных напитков (например, чая и кофе).
3.	Клиентский код — демонстрирует процесс приготовления различных напитков с использованием шаблонного метода.
Шаги выполнения:
1. Создайте базовый класс Beverage, который будет содержать шаблонный метод и общие шаги приготовления напитков:

public abstract class Beverage
{
    // Шаблонный метод
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }

    // Общий шаг для всех напитков
    private void BoilWater()
    {
        Console.WriteLine("Кипячение воды...");
    }

    // Общий шаг для всех напитков
    private void PourInCup()
    {
        Console.WriteLine("Наливание в чашку...");
    }

    // Абстрактные методы для шагов, которые будут реализованы в подклассах
    protected abstract void Brew();
    protected abstract void AddCondiments();
}

2. Создайте класс для приготовления чая, наследующий от Beverage, и реализуйте конкретные шаги:

public class Tea : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заваривание чая...");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавление лимона...");
    }
}

3. Создайте класс для приготовления кофе, также наследующий от Beverage, и реализуйте конкретные шаги:

public class Coffee : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Заваривание кофе...");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Добавление сахара и молока...");
    }
}

4. Напишите клиентский код, который будет демонстрировать процесс приготовления различных напитков:

class Program
{
    static void Main(string[] args)
    {
        // Приготовление чая
        Beverage tea = new Tea();
        Console.WriteLine("Приготовление чая:");
        tea.PrepareRecipe();

        Console.WriteLine();

        // Приготовление кофе
        Beverage coffee = new Coffee();
        Console.WriteLine("Приготовление кофе:");
        coffee.PrepareRecipe();
    }
}

Задания:
1.	Реализуйте код по шагам выше.
2.	Тестирование:
o	Проверьте процесс приготовления чая и кофе с выводом на экран.
o	Убедитесь, что каждый напиток выполняет свои специфические шаги (например, добавление лимона в чай и сахара в кофе).
3.	Расширение функционала:
o	Добавьте новый напиток, например, горячий шоколад, и реализуйте соответствующие шаги.
o	Добавьте в базовый класс возможность задать шаги, которые пользователь может пропускать (например, не добавлять сахар в кофе).
o	Реализуйте возможность кастомизации шагов (например, добавление разных типов молока в кофе).
4.	Обработка ошибок:
o	Реализуйте обработку ситуации, когда шаг не может быть выполнен (например, отсутствие ингредиентов).


Вопросы для самопроверки:
1.	Как работает шаблонный метод, и в чем его преимущество перед обычным наследованием?
2.	Как можно использовать хуки для настройки поведения подклассов?
3.	Какие еще ситуации могут потребовать использования паттерна "Шаблонный метод" в реальных проектах? */
/* using System;
using System.Collections.Generic;

namespace TemplateMethodBeverages
{
    class Inventory
    {
        private readonly Dictionary<string, int> s = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public Inventory Add(string k, int v) { s[k] = v; return this; }
        public bool Use(string k, int v = 1) { if (!s.TryGetValue(k, out var c) || c < v) return false; s[k] = c - v; return true; }
    }

    class BeverageOptions
    {
        public bool WithCondiments = true;
        public string MilkType = "обычное";
    }

    abstract class Beverage
    {
        protected readonly Inventory Inv;
        protected readonly BeverageOptions Opt;
        protected Beverage(Inventory inv, BeverageOptions opt = null) { Inv = inv; Opt = opt ?? new BeverageOptions(); }

        public void PrepareRecipe()
        {
            BoilWater();
            if (!Brew()) { Console.WriteLine("Ошибка: не удалось заварить"); return; }
            PourInCup();
            if (CustomerWantsCondiments())
            {
                if (!AddCondiments()) { Console.WriteLine("Ошибка: не удалось добавить добавки"); return; }
            }
            Customize();
            Console.WriteLine("Готово\n");
        }

        private void BoilWater() => Console.WriteLine("Кипячение воды");
        private void PourInCup() => Console.WriteLine("Наливание в чашку");
        protected abstract bool Brew();
        protected abstract bool AddCondiments();
        protected virtual bool CustomerWantsCondiments() => Opt.WithCondiments;
        protected virtual void Customize() { }
    }

    class Tea : Beverage
    {
        public Tea(Inventory inv, BeverageOptions opt = null) : base(inv, opt) { }
        protected override bool Brew()
        {
            if (!Inv.Use("tea")) return false;
            Console.WriteLine("Заваривание чая");
            return true;
        }
        protected override bool AddCondiments()
        {
            if (!Inv.Use("lemon")) return false;
            Console.WriteLine("Добавление лимона");
            return true;
        }
    }

    class Coffee : Beverage
    {
        public Coffee(Inventory inv, BeverageOptions opt = null) : base(inv, opt) { }
        protected override bool Brew()
        {
            if (!Inv.Use("coffee")) return false;
            Console.WriteLine("Заваривание кофе");
            return true;
        }
        protected override bool AddCondiments()
        {
            if (!Inv.Use("sugar")) return false;
            Console.WriteLine("Добавление сахара");
            if (!Inv.Use("milk")) return false;
            Console.WriteLine("Добавление молока");
            return true;
        }
        protected override void Customize()
        {
            Console.WriteLine($"Тип молока: {Opt.MilkType}");
        }
    }

    class HotChocolate : Beverage
    {
        public HotChocolate(Inventory inv, BeverageOptions opt = null) : base(inv, opt) { }
        protected override bool Brew()
        {
            if (!Inv.Use("cocoa")) return false;
            Console.WriteLine("Растворение какао");
            return true;
        }
        protected override bool AddCondiments()
        {
            if (!Inv.Use("marshmallow")) return false;
            Console.WriteLine("Добавление маршмеллоу");
            return true;
        }
    }

    class Program
    {
        static void Main()
        {
            var inv = new Inventory()
                .Add("tea", 2)
                .Add("lemon", 2)
                .Add("coffee", 2)
                .Add("sugar", 2)
                .Add("milk", 2)
                .Add("cocoa", 1)
                .Add("marshmallow", 1);

            Console.WriteLine("Приготовление чая:");
            var tea = new Tea(inv);
            tea.PrepareRecipe();

            Console.WriteLine("Приготовление кофе (молоко: миндальное):");
            var coffee = new Coffee(inv, new BeverageOptions { WithCondiments = true, MilkType = "миндальное" });
            coffee.PrepareRecipe();

            Console.WriteLine("Приготовление кофе без добавок:");
            var coffeeNoCond = new Coffee(inv, new BeverageOptions { WithCondiments = false });
            coffeeNoCond.PrepareRecipe();

            Console.WriteLine("Приготовление горячего шоколада:");
            var choco = new HotChocolate(inv);
            choco.PrepareRecipe();

            Console.WriteLine("Проверка ошибки (нет лимона):");
            var teaNoLemon = new Tea(inv);
            teaNoLemon.PrepareRecipe();
        }
    }
} */

/* Цель:
Изучить и реализовать паттерн Посредник (Mediator) на языке C#. Ваша задача — создать систему управления диалогом между несколькими участниками, где взаимодействие между участниками организовано через объект-посредник.
Описание задачи:
Необходимо реализовать чат-систему, где несколько участников могут отправлять сообщения друг другу, но вместо прямого взаимодействия между участниками будет использоваться посредник. Посредник отвечает за передачу сообщений между участниками и управляет всей коммуникацией.
Структура программы:
1.	Интерфейс Посредник (IMediator) — определяет контракт для посредника.
2.	Конкретный посредник (ConcreteMediator) — реализует интерфейс посредника и управляет взаимодействием участников.
3.	Интерфейс участника (IColleague) — определяет интерфейс для участников, которые будут взаимодействовать через посредника.
4.	Конкретные участники (ConcreteColleague) — участники чата, которые обмениваются сообщениями через посредника.
5.	Клиентский код — демонстрирует работу системы.
Шаги выполнения:
1. Создайте интерфейс IMediator, который будет описывать метод для отправки сообщений:
public interface IMediator
{
    void SendMessage(string message, Colleague colleague);
}

2. Создайте интерфейс или базовый класс для участников чата (коллег):
public abstract class Colleague
{
    protected IMediator _mediator;

    public Colleague(IMediator mediator)
    {
        _mediator = mediator;
    }

    public abstract void ReceiveMessage(string message);
}

3. Реализуйте конкретный класс посредника, который будет управлять обменом сообщений между участниками:
public class ChatMediator : IMediator
{
    private List<Colleague> _colleagues;

    public ChatMediator()
    {
        _colleagues = new List<Colleague>();
    }

    public void RegisterColleague(Colleague colleague)
    {
        _colleagues.Add(colleague);
    }

    public void SendMessage(string message, Colleague sender)
    {
        foreach (var colleague in _colleagues)
        {
            if (colleague != sender)
            {
                colleague.ReceiveMessage(message);
            }
        }
    }
}

4. Реализуйте конкретных участников чата, наследующихся от класса Colleague:

public class User : Colleague
{
    private string _name;

    public User(IMediator mediator, string name) : base(mediator)
    {
        _name = name;
    }

    public void Send(string message)
    {
        Console.WriteLine($"{_name} отправляет сообщение: {message}");
        _mediator.SendMessage(message, this);
    }

    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"{_name} получил сообщение: {message}");
    }
}

5. Напишите клиентский код для демонстрации работы чата:

class Program
{
    static void Main(string[] args)
    {
        // Создаем посредника
        ChatMediator chatMediator = new ChatMediator();

        // Создаем участников
        User user1 = new User(chatMediator, "Алиса");
        User user2 = new User(chatMediator, "Боб");
        User user3 = new User(chatMediator, "Чарли");

        // Регистрируем участников в чате
        chatMediator.RegisterColleague(user1);
        chatMediator.RegisterColleague(user2);
        chatMediator.RegisterColleague(user3);

        // Участники обмениваются сообщениями
        user1.Send("Привет всем!");
        user2.Send("Привет, Алиса!");
        user3.Send("Всем привет!");
    }
}

Задания:
1.	Реализуйте код по шагам выше.
2.	Тестирование:
o	Проверьте работу чата с тремя участниками. Убедитесь, что сообщение отправляется всем, кроме отправителя.
o	Добавьте возможность отправки приватных сообщений одному из участников.
3.	Расширение функционала:
o	Реализуйте возможность создания групп чатов, где посредник управляет несколькими чатами одновременно.
o	Добавьте логирование сообщений, чтобы посредник сохранял историю переписки.
4.	Обработка ошибок:
o	Добавьте обработку ошибок, например, если участник пытается отправить сообщение, не зарегистрировавшись в чате.
o	Реализуйте механизм отключения участника от чата.

Вопросы для самопроверки:
1.	В чем преимущество использования паттерна "Посредник" в системе с большим количеством участников?
2.	Как изменить посредника, чтобы он поддерживал отправку сообщений определённой группе участников?
3.	Какие задачи в реальной жизни могут быть решены с использованием паттерна "Посредник"? */
/* using System;
using System.Collections.Generic;

namespace MediatorSimpleChat
{
    public interface IMediator
    {
        void RegisterColleague(Colleague colleague);
        void UnregisterColleague(Colleague colleague);
        void SendMessage(string message, Colleague sender);
        void SendPrivate(string message, Colleague sender, string toName);
        void CreateGroup(string group);
        void JoinGroup(string group, Colleague c);
        void LeaveGroup(string group, Colleague c);
        void SendGroup(string group, string message, Colleague sender);
        void PrintLog();
    }

    public abstract class Colleague
    {
        protected IMediator _mediator;
        public string Name { get; }
        protected Colleague(IMediator mediator, string name) { _mediator = mediator; Name = name; }
        public abstract void ReceiveMessage(string message);
        public void Send(string message) { _mediator.SendMessage(message, this); }
        public void SendPrivate(string toName, string message) { _mediator.SendPrivate(message, this, toName); }
        public void SendToGroup(string group, string message) { _mediator.SendGroup(group, message, this); }
    }

    public class ChatMediator : IMediator
    {
        private readonly List<Colleague> _colleagues = new List<Colleague>();
        private readonly Dictionary<string, Colleague> _byName = new Dictionary<string, Colleague>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, HashSet<Colleague>> _groups = new Dictionary<string, HashSet<Colleague>>(StringComparer.OrdinalIgnoreCase);
        private readonly List<string> _log = new List<string>();

        public void RegisterColleague(Colleague colleague)
        {
            if (colleague == null || _byName.ContainsKey(colleague.Name)) { Log($"ERR register {colleague?.Name}"); return; }
            _colleagues.Add(colleague);
            _byName[colleague.Name] = colleague;
            BroadcastSystem($"{colleague.Name} вошёл в чат");
            Log($"REG {colleague.Name}");
        }

        public void UnregisterColleague(Colleague colleague)
        {
            if (colleague == null || !_byName.ContainsKey(colleague.Name)) { Log($"ERR unregister {colleague?.Name}"); return; }
            _colleagues.Remove(colleague);
            _byName.Remove(colleague.Name);
            foreach (var g in _groups.Values) g.Remove(colleague);
            BroadcastSystem($"{colleague.Name} покинул чат");
            Log($"UNREG {colleague.Name}");
        }

        public void SendMessage(string message, Colleague sender)
        {
            if (!IsRegistered(sender)) { WarnNotRegistered(sender); return; }
            foreach (var c in _colleagues) if (!ReferenceEquals(c, sender)) c.ReceiveMessage($"[{sender.Name}] {message}");
            Log($"PUB {sender.Name}: {message}");
        }

        public void SendPrivate(string message, Colleague sender, string toName)
        {
            if (!IsRegistered(sender)) { WarnNotRegistered(sender); return; }
            if (!_byName.TryGetValue(toName, out var to)) { sender.ReceiveMessage($"[system] пользователь {toName} не найден"); Log($"ERR PM {sender.Name}->{toName}"); return; }
            to.ReceiveMessage($"[pm {sender.Name}] {message}");
            Log($"PM {sender.Name}->{toName}: {message}");
        }

        public void CreateGroup(string group)
        {
            if (!_groups.ContainsKey(group)) _groups[group] = new HashSet<Colleague>();
            Log($"GROUP NEW {group}");
        }

        public void JoinGroup(string group, Colleague c)
        {
            if (!IsRegistered(c)) { WarnNotRegistered(c); return; }
            if (!_groups.ContainsKey(group)) CreateGroup(group);
            _groups[group].Add(c);
            GroupSystem(group, $"{c.Name} присоединился к группе {group}");
            Log($"GROUP JOIN {group}:{c.Name}");
        }

        public void LeaveGroup(string group, Colleague c)
        {
            if (!_groups.TryGetValue(group, out var set) || !set.Remove(c)) { Log($"ERR GROUP LEAVE {group}:{c?.Name}"); return; }
            GroupSystem(group, $"{c.Name} покинул группу {group}");
            Log($"GROUP LEAVE {group}:{c.Name}");
        }

        public void SendGroup(string group, string message, Colleague sender)
        {
            if (!IsRegistered(sender)) { WarnNotRegistered(sender); return; }
            if (!_groups.TryGetValue(group, out var set)) { sender.ReceiveMessage($"[system] группы {group} нет"); Log($"ERR GROUP SEND {group}"); return; }
            if (!set.Contains(sender)) { sender.ReceiveMessage($"[system] вы не в группе {group}"); Log($"ERR GROUP PERM {group}:{sender.Name}"); return; }
            foreach (var c in set) if (!ReferenceEquals(c, sender)) c.ReceiveMessage($"[{group}|{sender.Name}] {message}");
            Log($"GROUP MSG {group}|{sender.Name}: {message}");
        }

        public void PrintLog()
        {
            Console.WriteLine("Лог:");
            foreach (var l in _log) Console.WriteLine(l);
        }

        private bool IsRegistered(Colleague c) => c != null && _byName.ContainsKey(c.Name);
        private void WarnNotRegistered(Colleague c) { c?.ReceiveMessage("[system] вы не зарегистрированы"); Log($"ERR NOTREG {c?.Name}"); }
        private void BroadcastSystem(string msg) { foreach (var c in _colleagues) c.ReceiveMessage($"[system] {msg}"); }
        private void GroupSystem(string group, string msg) { if (_groups.TryGetValue(group, out var set)) foreach (var c in set) c.ReceiveMessage($"[system:{group}] {msg}"); }
        private void Log(string s) => _log.Add($"[{DateTime.Now:HH:mm:ss}] {s}");
    }

    public class User : Colleague
    {
        public User(IMediator mediator, string name) : base(mediator, name) { }
        public override void ReceiveMessage(string message) => Console.WriteLine($"{Name}: {message}");
    }

    class Program
    {
        static void Main()
        {
            var mediator = new ChatMediator();

            var d = new User(mediator, "Дархан");
            var p = new User(mediator, "Пётр");
            var a = new User(mediator, "Акжан");

            mediator.RegisterColleague(d);
            mediator.RegisterColleague(p);
            mediator.RegisterColleague(a);

            d.Send("Привет всем");
            p.SendPrivate("Дархан", "Привет, как дела?");
            a.Send("Я здесь");

            mediator.CreateGroup("dev");
            mediator.JoinGroup("dev", d);
            mediator.JoinGroup("dev", p);
            d.SendToGroup("dev", "Созвон в 19:00?");
            a.SendToGroup("dev", "Тоже приду");

            mediator.LeaveGroup("dev", p);
            mediator.UnregisterColleague(p);
            p.Send("Слышно меня?");

            mediator.PrintLog();
        }
    }
} */

