using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheets.Infrastructure.Validation
{
	/// <summary>Модель ошибок</summary>
	public class ErrorModel
	{
		/// <summary>Список ошибок</summary>
		public Dictionary<string, string> Errors { get; set; }

		/// <summary>Сообщение</summary>
		public String Message { get; set; }
	}
}
