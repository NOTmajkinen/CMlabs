**BigNumberLibrary**

A library that can handle "*Big*" Integer numbers with unlimited bit depth. 

***A bit of story***

Ordinary data types, supported by programming languages, cannot handle numbers that exceed a certain limit.
Therefore, *long arithmetic* was invented to avoid this.

***What is this?***

> In computer science, **arbitrary-precision arithmetic** indicates that calculations are performed on numbers whose digits of precision
> are limited only by the available memory of the host system. [Wikipedia](https://en.wikipedia.org/wiki/Arbitrary-precision_arithmetic)

***How it works?***

A special structure that can handle numbers like this is created. It represents numbers by array, list, etc. After that we just simply perform our 
algorithms to work with this numbers.

***BigNumberLibrary part***

A library supports common arithmetic operations ('+', '-', '*', '/', '%')

***Examples:***

`BigNumber a = BigNumber.Parse("123456789987654321321654987"); \\ creating a BigNumber number`

`BigNumber b = BigNumber.Parse("78945613207894651389456546");`

`Console.WriteLine(a + b); \\ "202402403195548972711111533"`

`Console.WriteLine(a - b); \\ "44511176779759669932198441"`

And other operations. Also it has *bitwise shifts* **('<<', '>>')**, *exponentation* **(Power())**, *finding the maximum value* **(Max())**
and *Boolean operations* **('==', '!=', '>', '<', '>=', '<=')**.

***Examples:***

`Console.WriteLine(BigNumber.Max(a, b)); \\ returns a`

`Console.WriteLine(a > b); \\ true`

`int c = 20; \\ other operations works with integer number as the second argument`

`Console.WriteLine(BigNumber.Power(a, c)); \\ value`

`Console.WriteLine(a << b); \\ value`

