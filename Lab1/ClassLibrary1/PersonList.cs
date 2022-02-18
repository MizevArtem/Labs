using System;

namespace ClassLibrary1
{
    /// <summary>
    /// Класс листа людей 
    /// Включает в себя информацию о людях (имена, фамилии, явки, пароли)
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Массив людей
        /// </summary>
        private Person[] _persons = new Person[0];

        /// <summary>
        /// Добавление человека в список
        /// </summary>
        /// <param name="person">Экземпляр класса Person</param>
        public void AddPerson(Person person)
        {
            Array.Resize(ref _persons, _persons.Length + 1);
            _persons[_persons.Length - 1] = person;
        }

        /// <summary>
        /// Удаление содержимого списка
        /// </summary>
        public void DeleteAllPeople()
        {
            Array.Resize(ref _persons, 0);
        }

        /// <summary>
        /// Удаление человека по индексу в массиве
        /// </summary>
        /// <param name="index">Индекс Person-ы в массиве
        public void DeleteByIndex(int index)
        {
            //TODO: дубль
            if (index < 0 || index > _persons.Length - 1)
            {
                throw new Exception($"Отсутствует запись под номером {index + 1}");
            }
            var bufferArray = _persons;
            int position = 0;
            _persons = new Person[_persons.Length - 1];
            for (int i = 0; i < bufferArray.Length; i++)
            {
                if (i == index) continue;

                _persons[position] = bufferArray[i];
                position++;
            }
        }
        
        /// <summary>
        /// Удаление человека из списка по имени и фамилии
        /// </summary>
        /// <param name="name">Имя удаялемого человека</param>
        /// <param name="lastName">Фамилия удаляемого человека</param>
        public bool DeletePersonByAnthroponym(string name, string lastName)
        {
            Person[] bufferArray = new Person[0];
            bool deleted = false;
            for (int i = 0; i < _persons.Length; i++)
            {
                if (_persons[i].Name == name 
                    || _persons[i].LastName == lastName) continue;
                Array.Resize(ref bufferArray, bufferArray.Length + 1);
                bufferArray[bufferArray.Length - 1] = _persons[i];
                deleted = true;
            }
            _persons = bufferArray;
            return deleted;
        }

        /// <summary>
        /// Количество людей в списке
        /// </summary>
        public int CountOfPersons => _persons.Length;

        /// <summary>
        /// Получение элемента по индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Возвращение элементы по индексу</returns> 
        public Person GetByIndex(int index)
        {
            if (index >= 0 && index < _persons.Length)
            {
                return _persons[index];
            }
            else
            {
                //TODO: дубль
                throw new Exception("Не существует записи с данным номером");
            }
        }
    }
}
