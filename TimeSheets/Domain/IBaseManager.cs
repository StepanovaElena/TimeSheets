using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheets.Domain
{
	/// <summary> Менеджер запросов к репозиториям </summary>
	public interface IBaseManager<TModel, TRequest>
	{
		/// <summary> Возвращает объект по его Id </summary>
		/// <param name="id"> Id объекта </param>
		/// <returns> Искомый объект </returns>
		Task<TModel> GetItem(Guid id);

		/// <summary> Возвращает несколько объектов </summary>
		/// <returns> Коллекция содержащая объекты </returns>
		Task<IEnumerable<TModel>> GetItems();

		/// <summary> Создает новый объект в репозитории </summary>
		/// <param name="request"> Запрос на создание объекта </param>
		/// <returns> Id созданного обхекта </returns>
		Task<Guid> Create(TRequest request);

		/// <summary> Изменяет существующий объект </summary>
		/// <param name="id"> Id изменямого объекта </param>
		/// <param name="request"> Запрос на изменение объекта </param>
		Task Update(Guid id, TRequest request);

		/// <summary> Удаляет объект из репозитория </summary>
		/// <param name="id"> Id удаляемого объекта </param>
		Task Delete(Guid id);
	}
}
