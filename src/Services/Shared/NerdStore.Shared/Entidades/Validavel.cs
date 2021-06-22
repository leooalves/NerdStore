
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Shared.Entidades
{
    public abstract class Validavel
    {
        public Validavel()
        {
            _notifications = new List<Notification>();
        }

        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public bool EhValido => !_notifications.Any();

        public bool EhInvalido => !EhValido;

        public void Validar<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            var validationResult = validator.Validate(model);
            AddNotifications(validationResult);
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(IList<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ICollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorCode, error.ErrorMessage);
            }
        }

    }
}
