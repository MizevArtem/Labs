// See https://aka.ms/new-console-template for more information
using ClassLibrary1;
MyRandom rnd = new MyRandom(System.DateTime.Now.Millisecond);
PersonList personList = new PersonList();
const int countElements = 15;
for (int i = 0; i < countElements; i++)
{
    if (rnd.Next(false, true))
    {
        personList.AddPerson(Adult.GetRandomPerson());
    }
    else
    {
        personList.AddPerson(Child.GetRandomPerson());
    }
}
SortingList(ref personList);
PrintList(personList);
var person = personList.GetByIndex(3);
switch (person)
{
    case Adult adult:
        Adult partner = adult.GetPartner();
        PrintPerson("Partner is ", partner);
        break;
    case Child child:
        (Adult Father, Adult Mother) = child.GetParents();
        PrintPerson("Father:", Father);
        PrintPerson("Mother:", Mother);
        break;
}

Console.ReadLine();

static void PrintList(PersonList personList)
{
    for (int i = 0; i < countElements; i++)
    {
        PrintPerson($"{i + 1}) ", personList.GetByIndex(i));
    }
}

static void PrintPerson(string prefix, PersonBase person)
{
    if (person is not null)
    {
        Console.WriteLine(prefix + person.PersonInfo());
    }
    else
    {
        Console.WriteLine(prefix + "missing person");
    } 
}

static void SortingList(ref PersonList personList)
{
    Random rnd = new Random();
    PersonList mansList = new PersonList();
    PersonList womansList = new PersonList();
    PersonList childrensList = new PersonList();
    for (int i = 0; i < personList.CountOfPersons; i++)
    {
        PersonBase person = personList.GetByIndex(i);
        if (person is Adult)
        {
            if (person.Gender == PossibleGender.Male)
            {
                mansList.AddPerson(person);
            }
            else 
            {
                womansList.AddPerson(person);
            }
        }
        else
        {
            childrensList.AddPerson(person);
        }
    }
    int countMan = mansList.CountOfPersons;
    Adult husband = null;
    int countChildren = 0;
    for (int i = 0; i < countMan; i++)
    {
        int countWomans = womansList.CountOfPersons;
        countChildren = childrensList.CountOfPersons;
        husband = mansList.GetByIndex(i) as Adult;
        Adult wife = null;
        if (countWomans != 0)
        {
            int nextWoman = rnd.Next(0, countWomans);
            wife = (womansList.GetByIndex(nextWoman)) as Adult;
            husband.MaritalStatus = MaritalStatus.Married;
            husband.Partner = wife;
            wife.MaritalStatus = MaritalStatus.Married;
            wife.Partner = husband;
            womansList.DeleteByIndex(nextWoman);
        }
        int chilrenInFamily = rnd.Next(0, 3);
        if (chilrenInFamily > countChildren)
        {
            chilrenInFamily = countChildren;
        }
        for (int j = 0; j < chilrenInFamily; j++)
        {
            int numberOfChild = rnd.Next(0, countChildren);
            Child child = (childrensList.GetByIndex(numberOfChild) as Child);
            child.Father = husband;
            child.Mother = wife;
        }
    }
    if(countChildren > 0)
    {
        for (int i = 0; i < countChildren; i++)
        {
            Child child = (childrensList.GetByIndex(i) as Child);
            child.Father = husband;
            if (husband.Partner is not null)
            {
                child.Mother = husband.Partner;
            }
        }
    }

}