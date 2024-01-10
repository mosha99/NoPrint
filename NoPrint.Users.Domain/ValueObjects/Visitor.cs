using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Tools;

namespace NoPrint.Users.Domain.ValueObjects;

public class Visitor
{
    private const int CodeExpireMin = 3;
    private Visitor() { }

    public static Visitor CreateInstance(string phoneNumber)
    {
        return new Visitor()
        {
            PhoneNumber = phoneNumber
        };
    }

    public string PhoneNumber { get; private set; }
    public int? Code { get; private set; }
    public DateTime? CodeGenerateTime { get; private set; }

    public void Clear()
    {
        CodeGenerateTime = null;
        Code = null;
    }

    public string GenerateCode()
    {
        var ensureCode = new TimeScopeCodeGenerator(CodeExpireMin).GetCode();

        return GenerateCode(ensureCode, false);
    }
    public void Validate(string code)
    {
        CodeGenerateTime.ValidationCheck(x => x is not null, "E1032");
        CodeGenerateTime.Value.AddMinutes(CodeExpireMin).ValidationCheck(x => x > DateTime.Now, "E1028");
        bool isValid = new TimeScopeCodeGenerator(CodeExpireMin).CodeIsValid(code, GenerateCode);
        isValid.ValidationCheck(x => x, "E1027");
    }
    private string GenerateCode(int ensureCode) => GenerateCode(ensureCode, true);
    private string GenerateCode(int ensureCode, bool forValidate)
    {
        if (!forValidate)
        {
            Code = new Random().Next(1000, 9999);
            CodeGenerateTime = DateTime.Now;
        }

        var numberCode = 1;

        PhoneNumber.Where(x => x != '0').ToList().ForEach(x => numberCode *= x);

        string result = (Code * numberCode * ensureCode).ToString();

        var rl = result.Take(4).ToList();

        rl.Add(result[^1]);
        rl.Add(result[^3]);

        if (rl[3] == '0') rl[3] = rl[2];
        if (rl[3] == '0') rl[3] = '6';

        return $"{rl[3]}{rl[5]}{rl[1]}{rl[4]}";
    }
}