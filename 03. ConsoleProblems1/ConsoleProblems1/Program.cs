string s = "AAAv";

s = s.ToLower();

string result = "";

foreach (char c in s)
{
    if (char.IsLetterOrDigit(c))
    {
        result += c;
    }
}

Console.WriteLine(result);

bool matched = true;

int left = 0;
int right = result.Length - 1;

while (left < right)
{
    if (result[left] != result[right])
    {
        matched = false;
        break;
    }

    left++;
    right--;
}

Console.WriteLine(matched);