using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        bool credit1 = random.Next(2) == 1;
        bool credit2 = random.Next(2) == 1;
        bool credit3 = random.Next(2) == 1;
        Console.WriteLine($"Заліки: {credit1}, {credit2}, {credit3}");
        StudZ studentZ = new StudZ(credit1, credit2, credit3);

        Za4et za4et = new Za4et(studentZ);
        Prepod prepod = new Prepod();
        za4et.Event1 += prepod.OnEvent1;
        za4et.Event2 += prepod.OnEvent2;
        za4et.CheckCredits();

        if (za4et.Passed)
        {
            int exam1 = random.Next(2, 6);
            int exam2 = random.Next(2, 6);
            int exam3 = random.Next(2, 6);
            Console.WriteLine($"Екзамени: {exam1}, {exam2}, {exam3}");
            StudE studentE = new StudE(exam1, exam2, exam3);

            Exam exam = new Exam(studentE);
            exam.Event3 += prepod.OnEvent3;
            exam.Event4 += prepod.OnEvent4;
            exam.CheckExams();
        }
    }
}

class StudZ
{
    public bool Credit1 { get; private set; }
    public bool Credit2 { get; private set; }
    public bool Credit3 { get; private set; }

    public StudZ(bool credit1, bool credit2, bool credit3)
    {
        Credit1 = credit1;
        Credit2 = credit2;
        Credit3 = credit3;
    }
}

class StudE
{
    public int Exam1 { get; private set; }
    public int Exam2 { get; private set; }
    public int Exam3 { get; private set; }

    public StudE(int exam1, int exam2, int exam3)
    {
        Exam1 = exam1;
        Exam2 = exam2;
        Exam3 = exam3;
    }
}

class Za4et
{
    public StudZ Student { get; private set; }
    public bool Passed { get; private set; }

    public event Action Event1;
    public event Action Event2;

    public Za4et(StudZ student)
    {
        Student = student;
    }

    public void CheckCredits()
    {
        if (Student.Credit1 && Student.Credit2 && Student.Credit3)
        {
            Passed = true;
            Event1?.Invoke();
        }
        else
        {
            Passed = false;
            Event2?.Invoke();
        }
    }
}

class Exam
{
    public StudE Student { get; private set; }

    public event Action Event3;
    public event Action Event4;

    public Exam(StudE student)
    {
        Student = student;
    }

    public void CheckExams()
    {
        if (Student.Exam1 > 2 && Student.Exam2 > 2 && Student.Exam3 > 2)
        {
            Event3?.Invoke();
        }
        else
        {
            Event4?.Invoke();
        }
    }
}

class Prepod
{
    public void OnEvent1()
    {
        Console.WriteLine("Заліки здав. Допускається до іспитів");
    }

    public void OnEvent2()
    {
        Console.WriteLine("Не всі заліки здав. До іспитів не допускається");
    }

    public void OnEvent3()
    {
        Console.WriteLine("Іспити склав. Перекладено на наступний курс");
    }

    public void OnEvent4()
    {
        Console.WriteLine("Не всі іспити склав. На перездачу");
    }
}
