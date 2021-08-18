using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AppDev.Sapper.Model.Enums;
using AppDev.Sapper.Model.Ext;
using PostSharp.Patterns.Contracts;
using static System.IO.Path;

namespace AppDev.Sapper.Utils.UI
{
	/// <summary>
	///    Cell image converter
	///    <remarks>Converts <see cref="MinefieldCellState" /> into image path</remarks>
	/// </summary>
	public sealed class CellImageConverter : IValueConverter
	{
		/// <inheritdoc />
		public object Convert([Required] object value, Type targetType, [Required] object parameter, CultureInfo culture)
		{
			Contract.Requires(parameter is string);

			MinefieldCellState cellState;
			if (!Enum.TryParse(value?.ToString(), out cellState))
				throw new ArgumentException($"Cannot parse {value} to MinefieldCellState type", nameof(cellState));

			var imageRootDir = Combine(Directory.GetCurrentDirectory(), (string) parameter);
			if (!Directory.Exists(imageRootDir))
				throw new ArgumentException($"Directory {imageRootDir} not exists", nameof(imageRootDir));

			var imageFile = cellState.GetImageFile();
			var questionImagePath = Combine(imageRootDir, imageFile);
			if (!File.Exists(questionImagePath))
				throw new ArgumentException($"File {questionImagePath} not exists", nameof(imageRootDir));

			return new BitmapImage(new Uri(questionImagePath));
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> default(object);
	}
}