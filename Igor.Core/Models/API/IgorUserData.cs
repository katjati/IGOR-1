using Igor.Library.DataAccess;
using Igor.Library.Models;
using Igor.Library.Services;

namespace Igor.Core.Models.API
{
    public class IgorUserData
    {
        #region Properties

        /// <summary>
        /// ID in DB.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Domain Username.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User e-mail.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Determines whether a user uses Vasont.
        /// </summary>
        public string AccreditationIdentifier { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Empty instance.
        /// </summary>
        public IgorUserData() { }
        /// <summary>
        /// Full instance.
        /// </summary>
        /// <param name="input"></param>
        public IgorUserData(IgorUser input, LarpUnitOfWork uow) : this()
        {
            IgorDataService dataService = new IgorDataService(uow);
            if (input != null)
            {
                Email = input.Email;
                UserName = input.UserName;
                UserId = input.Id;
                AccreditationIdentifier = dataService.GetActiveAccreditationByUser(input.Id)?.AccreditationIdentifier;
            }
        }

        #endregion
    }
}