using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //TODO:
            _persons[_persons.Length - 1] = (Person)person.Clone();
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
        /// <<param name="index">Индекс Person-ы в массиве
        public void DeleteByIndex(int index)
        {
            if (index < 0 || index > _persons.Length - 1)
            {
                throw new Exception("Index does not exists");
            }
            var bufferArray = _persons;
            //TODO: RSDN
            int pos = 0;
            _persons = new Person[_persons.Length - 1];
            for (int i = 0; i < bufferArray.Length; i++)
            {
                if (i != index)
                {
                    _persons[pos] = bufferArray[i];
                    pos++;
                }
            }
        }

        //TODO: RSDN
        /// <summary>
        /// Удаление человека из списка по имени и фамилии
        /// </summary>
        /// <param name="name">Имя удаялемого человека</param>
        /// <param name="lastName">Фамилия удаляемого человека</param>
        public void DeletePersonByName(string name, string lastName)
        {
            //TODO: RSDN
            Person[] BufferArray = new Person[0];
            for (int i = 0; i < _persons.Length; i++)
            {
                if (_persons[i].Name == name 
                    || _persons[i].LastName == lastName) continue;

                Array.Resize(ref BufferArray, BufferArray.Length + 1);
                BufferArray[BufferArray.Length - 1] = _persons[i];
            }
            _persons = BufferArray;
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
                //TODO:
                throw new Exception("Index does not exists");
            }
        }
    }
}
