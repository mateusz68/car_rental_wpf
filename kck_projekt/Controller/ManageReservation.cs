using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kck_projekt.Controller
{
    public class ManageReservation
    {
        private Model.AppContext model { get; set; }
        private ViewInterface view { get; set; }
        public ManageReservation(Model.AppContext model, ViewInterface view)
        {
            this.model = model;
            this.view = view;
        }

        public List<Model.Reservation> GetReservationList()
        {
            return model.Reservations.ToList();
        }

        public List<string> GetCarReservationDates(int car_id)
        {
            List<string> tempList = new List<string>();
            List<Model.Reservation> reservations = model.Reservations
                .Where(r => r.Car.CarId == car_id)
                .Where(r => r.DateTo >= DateTime.Now)
                .OrderBy(r => r.DateFrom).ToList();
            foreach(Model.Reservation res in reservations)
            {
                var tempStr = res.DateFrom.ToString() + " -- " + res.DateTo.ToString();
                tempList.Add(tempStr);
            }
            return tempList;
        }
        public bool CheckPersonalDataExist(int userId)
        {
            var tempUser = model.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (tempUser != null)
            {
                if (string.IsNullOrEmpty(tempUser.Adres1))
                    return false;
                if (string.IsNullOrEmpty(tempUser.Adres2))
                    return false;
                if (string.IsNullOrEmpty(tempUser.Adres3))
                    return false;
                if (string.IsNullOrEmpty(tempUser.Name))
                    return false;
                if (string.IsNullOrEmpty(tempUser.Surname))
                    return false;
                if (tempUser.Phone == 0)
                    return false;
                return true;
            }
            return false;
        }

        public void AddReservation(Model.Reservation reservationData)
        {
            if (!CheckPersonalDataExist(reservationData.User.UserId))
            {
                view.ShowMessage("Błąd. Uzupełnij swoje dane osobiste!");
                return;
            }

            var temp = model.Reservations
                .Where(m => m.Car.CarId == reservationData.Car.CarId)
                .Where(f => (f.DateFrom >= reservationData.DateFrom && f.DateFrom <= reservationData.DateTo) || (f.DateTo >= reservationData.DateFrom && f.DateTo <= reservationData.DateTo))
                .FirstOrDefault();
            if (temp == null)
            {
                TimeSpan diff = reservationData.DateTo - reservationData.DateFrom;
                double timeDiffDouble = Math.Abs(diff.TotalDays);
                timeDiffDouble = Math.Round(timeDiffDouble, MidpointRounding.AwayFromZero);
                if (timeDiffDouble < 1)
                {
                    timeDiffDouble = 1;
                }
                decimal cost = (int)timeDiffDouble * reservationData.Car.CarDayPrince;
                reservationData.Cost = cost;
                model.Reservations.Add(reservationData);
                model.SaveChanges();
                view.ShowMessage("Samochód zarezerwowany pomyślnie.");
                return;
            }
            view.ShowMessage("Błąd. Wybrany termin koliduje z inną rezerwacją.");
        }

        public List<Model.Reservation> GetUserReservation(int userId)
        {
            return model.Reservations.Where(r => r.User.UserId == userId).ToList();
        }

        public void DeleteReservation(int reservationId)
        {
            var tempReservation = model.Reservations.SingleOrDefault(m => m.ReservationId == reservationId);
            if (tempReservation != null)
            {
                model.Reservations.Remove(tempReservation);
                model.SaveChanges();
                view.ShowMessage("Pomyślnie usunięto rezerwację " + tempReservation);
                return;
            }
            view.ShowMessage("Nie udało się usunąć rezerwacji.");
        }

        public void SaveReservation(Model.Reservation reservationData, int editReservationId)
        {
            var temp = model.Reservations
                .Where(m => m.Car.CarId == reservationData.Car.CarId)
                .Where(m => m.ReservationId != editReservationId)
                .Where(f => (f.DateFrom >= reservationData.DateFrom && f.DateFrom <= reservationData.DateTo) || (f.DateTo >= reservationData.DateFrom && f.DateTo <= reservationData.DateTo))
                .FirstOrDefault();
            if (temp == null)
            {
                TimeSpan diff = reservationData.DateTo - reservationData.DateFrom;
                double timeDiffDouble = Math.Abs(diff.TotalDays);
                timeDiffDouble = Math.Round(timeDiffDouble, MidpointRounding.AwayFromZero);
                if(timeDiffDouble < 1)
                {
                    timeDiffDouble = 1;
                }
                decimal cost = (int)timeDiffDouble * reservationData.Car.CarDayPrince;
                if(editReservationId == -1)
                {
                    reservationData.Cost = cost;
                    model.Reservations.Add(reservationData);
                    model.SaveChanges();
                    view.ShowMessage("Samochód zarezerwowany pomyślnie.");
                    return;
                }
                else
                {
                    var tempReservation = model.Reservations.Where(r => r.ReservationId == editReservationId).FirstOrDefault();
                    tempReservation.DateFrom = reservationData.DateFrom;
                    tempReservation.DateTo = reservationData.DateTo;
                    tempReservation.Car = reservationData.Car;
                    tempReservation.Comments = reservationData.Comments;
                    tempReservation.Cost = cost;
                    tempReservation.User = reservationData.User;
                    view.ShowMessage("Rezerwacja zmieniona pomyślnie.");
                    model.SaveChanges();
                    return;
                }
                view.ShowMessage("Nie udało się dodać rezerwacji.");
                return;
            }
            view.ShowMessage("Błąd. Wybrany termin koliduje z inną rezerwacją.");
        }
    }
}
