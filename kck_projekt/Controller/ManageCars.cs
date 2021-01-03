using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Controller
{
    public class ManageCars
    {
        private Model.AppContext model { get; set; }
        private ViewInterface view { get; set; }
        public ManageCars(Model.AppContext model, ViewInterface view)
        {
            this.model = model;
            this.view = view;
        }

        #region Mark Operation
        public bool MarkExsist(String name)
        {
            var mark = model.Marks
                .Where(b => b.MarkName == name)
                .FirstOrDefault();
            if (mark != null)
            {
                return true;
            }
            return false;
        }
        public bool MarkExsist(int id)
        {
            var mark = model.Marks
                .Where(b => b.MarkId == id)
                .FirstOrDefault();
            if (mark != null)
            {
                return true;
            }
            return false;
        }
        public void AddMark(String name)
        {
            if (!MarkExsist(name))
            {
                model.Marks.Add(new Model.CarMark { MarkName = name });
                model.SaveChanges();
                view.ShowMessage("Pomyślnie dodano nową markę");
                return;
            }
            view.ShowMessage("Ta marka już istnieje.");
        }
        public void EditMark(String name, int id)
        {
            if(MarkExsist(id))
            {
                var mark = model.Marks.SingleOrDefault(m => m.MarkId == id);
                mark.MarkName = name;
                model.SaveChanges();
                view.ShowMessage("Pomyślnie zmienione nazwę marki na " + name);
                return;
            }
             view.ShowMessage("Nie udało się zmienić nazwy marki.");
        }

        public void DeleteMark(int id)
        {
            var mark = model.Marks.SingleOrDefault(m => m.MarkId == id);
            if(mark != null)
            {
                model.Marks.Remove(mark);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie usunięto markę " + mark.MarkName);
                return;
            }
            view.ShowMessage("Nie udało się usunąć marki.");
        }
        public List<Model.CarMark> GetMarkList()
        {
            return model.Marks.ToList();
        }
        #endregion

        #region Model Operation
        public bool ModelExsist(String name)
        {
            var tempModel = model.Models
                .Where(b => b.ModelName == name)
                .FirstOrDefault();
            if (tempModel != null)
            {
                return true;
            }
            return false;
        }
        public bool ModelExsist(int id)
        {
            var tempModel = model.Models
                .Where(b => b.ModelId == id)
                .FirstOrDefault();
            if (tempModel != null)
            {
                return true;
            }
            return false;
        }
        public void AddModel(string name, int markId)
        {
            if (!ModelExsist(name))
            {
                var mark = model.Marks.SingleOrDefault(m => m.MarkId == markId);
                model.Models.Add(new Model.CarModel { ModelName = name, Mark = mark });
                model.SaveChanges();
                view.ShowMessage("Pomyślnie dodano nowy model");
                return;
            }
            view.ShowMessage("Nie udało się dodać nowego modelu;");
        }
        public void EditModel(String name, int modelId, int markId)
        {
            if (ModelExsist(modelId))
            {
                var mark = model.Marks.SingleOrDefault(m => m.MarkId == markId);
                var tempModel = model.Models.SingleOrDefault(b => b.ModelId == modelId);
                tempModel.ModelName = name;
                tempModel.Mark = mark;
                model.SaveChanges();
                view.ShowMessage("Pomyślnie zmienione nazwę marki na " + name);
                return;
            }
            view.ShowMessage("Nie udało się zmienić nazwy marki.");
        }

        public void DeleteModel(int id)
        {
            var tempModel = model.Models.SingleOrDefault(m => m.ModelId == id);
            if (tempModel != null)
            {
                model.Models.Remove(tempModel);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie usunięto model " + tempModel.ModelName);
                return;
            }
            view.ShowMessage("Wybrany model nie istnieje.");
        }

        public List<Model.CarModel> GetModelsList()
        {
            return model.Models.ToList();
        }
        #endregion

        #region CarManage
        public void DeleteCar(int carId)
        {
            var tempCar = model.Cars.SingleOrDefault(m => m.CarId == carId);
            if (tempCar != null)
            {
                model.Cars.Remove(tempCar);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie usunięto samochód " + tempCar);
                return;
            }
            view.ShowMessage("Wybrany samochód nie istnieje.");
        }

        public bool CarExsist(int id)
        {
            var tempCar = model.Cars
                .Where(b => b.CarId == id)
                .FirstOrDefault();
            if (tempCar != null)
            {
                return true;
            }
            return false;
        }

        public void SaveCar(Model.Car carData, int editCarId)
        {
            if (editCarId == -1)
            {
                model.Cars.Add(carData);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie dodano samochód " + carData);
            }
            else
            {
                var tempCar = model.Cars.SingleOrDefault(c => c.CarId == editCarId);
                if(tempCar != null)
                {
                    tempCar.CarBail = carData.CarBail;
                    tempCar.Model = carData.Model;
                    tempCar.CarColor = carData.CarColor;
                    tempCar.CarDayPrince = carData.CarDayPrince;
                    tempCar.CarPower = carData.CarPower;
                    tempCar.CarRegNumb = carData.CarRegNumb;
                    tempCar.Engine = carData.Engine;
                    tempCar.Gearbox = carData.Gearbox;
                    tempCar.Status = carData.Status;
                    model.SaveChanges();
                    view.ShowMessage("Edycja samochodu zakończona pomyślnie.");
                }
            }
        }

        public List<Model.Car> GetCarList()
        {
            return model.Cars.ToList<Model.Car>();
        }
        public List<Model.Car> GetAvaliableCarList()
        {
            return model.Cars
                .Where(c => c.Status == Model.CarStatus.Available)
                .ToList();
        }
        #endregion
    }
}
