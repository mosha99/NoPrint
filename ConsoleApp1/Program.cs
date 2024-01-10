


using NoPrint.Ef.Context;
using NoPrint.Users.Domain;


UserBase user = new UserBase(new UserExpireDate());


Visitor v = Visitor.CreateInstance("09013231042");

while (true)
{
    string code = v.GenerateCode();

    Console.WriteLine($"generated Code is {code}");
    var st = DateTime.Now;
    Console.WriteLine($"Start Valid Time {st}");

    while (true)
    {
        try
        {
            v.Validate(code);
            await Task.Delay(999);
        }
        catch (Exception e)
        {
            break;
        }
    }

    Console.WriteLine($"End Valid Time {DateTime.Now}");

    Console.WriteLine($"total minute is {(DateTime.Now - st).TotalMinutes} ------------------------------------------------");

    await Task.Delay(1000);

}

//var code = new TimeScopeCodeGenerator(5).GetCode();

//while (true)
//{
//    var v = new TimeScopeCodeGenerator(5).CodeIsValid(code, x => x);
//    Console.WriteLine($"-c {code} -v {v} -t {DateTime.Now}");
//    await Task.Delay(1000);

//}

//Console.WriteLine("Hello, World!");
