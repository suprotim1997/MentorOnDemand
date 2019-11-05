using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOD_DAL;
using System.Data.Entity;
using System.Net.Http;
using System.Data;
using System.Security.Cryptography;

namespace MOD_BAL
{
  public  class user
    {

        public samratEntities samrat = new samratEntities();
        
        // Get All 

        public List<UserDtl> getAllRegistered()
        {
            try
            {
                return samrat.UserDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<SkillDtl> getAllSkills()
        {
            try
            {
                return samrat.SkillDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<TrainingDtl> getAllTraining()
        {
            try
            {
                return samrat.TrainingDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<PaymentDtl> getAllPayment()
        {
            try
            {
                return samrat.PaymentDtls.ToList();
            }
            catch
            {
                throw;
            }
        }

        // Get All By ID 

        public UserDtl getUserById(int id)
        {
           try
            {
                return samrat.UserDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public SkillDtl getSkillById(int id)
        {
            try
            {
                return samrat.SkillDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public TrainingDtl getTrainingById(int id)
        {
            try
            {
                return samrat.TrainingDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public PaymentDtl getPaymentById(int id)
        {
            try
            {
                return samrat.PaymentDtls.Find(id);
            }
            catch
            {
                throw;
            }
        }

        // Get Search Data 

        public List<UserDtl> getSearchData(string trainerTechnology)
        {
            try
            {
                List<UserDtl> tt;
                tt = samrat.UserDtls.Where(x => x.trainerTechnology == trainerTechnology).ToList();
                return tt;
            }
            catch
            {
                throw;
            }
        }

        // Post Data In samrat 

        public UserDetails saveUser(UserDtl userDtl)
        {
            try
            {
                string message = null;
                UserDtl user1 = samrat.UserDtls.SingleOrDefault(x => x.email == userDtl.email);
                if (user1 == null)
                {

                    var pass = EncodePasswordToBase64(userDtl.password);

                    if (userDtl.role == 1)
                    {
                        userDtl.active = true;
                        var user = new UserDtl()
                        {
                            active = true,
                            userName = userDtl.userName.Trim().ToLower(),
                            email = userDtl.email.Trim().ToLower(),
                            firstName = userDtl.firstName.Trim(),
                            lastName = userDtl.lastName.Trim(),
                            role = userDtl.role,
                            password = pass
                        };

                        samrat.UserDtls.Add(user);

                        samrat.SaveChanges();
                        message = "Registered Successfully";
                    }
                    else if (userDtl.role == 2)
                    {
                        var user = new UserDtl();

                        user.active = true;
                        user.role = userDtl.role;
                        user.userName = userDtl.userName.Trim().ToLower();
                        user.email = userDtl.email.Trim().ToLower();
                        user.firstName = userDtl.firstName.Trim();
                        user.lastName = userDtl.lastName.Trim();
                        user.contactNumber = userDtl.contactNumber;
                        user.yearOfExperience = userDtl.yearOfExperience;
                        user.linkedinUrl = userDtl.linkedinUrl;
                        user.trainerTimings = userDtl.trainerTimings;
                        user.trainerTechnology = userDtl.trainerTechnology;
                        user.password = pass;
                        user.confirmedSignUp = userDtl.confirmedSignUp;

                        samrat.UserDtls.Add(user);

                        samrat.SaveChanges();
                        message = "Registered Successfully";

                    }
                    else if (userDtl.role == 3)
                    {
                        var user = new UserDtl();

                        user.active = true;
                        user.role = userDtl.role;
                        user.userName = userDtl.userName.Trim().ToLower();
                        user.email = userDtl.email.Trim().ToLower();
                        user.firstName = userDtl.firstName.Trim();
                        user.lastName = userDtl.lastName.Trim();
                        user.contactNumber = userDtl.contactNumber;
                        user.password = pass;
                        user.confirmedSignUp = userDtl.confirmedSignUp;

                        samrat.UserDtls.Add(user);

                        samrat.SaveChanges();
                        message = "Registered Successfully";
                    }
                }
                else
                {
                    message = "Email Already Exists";
                }
                return new UserDetails()
                {
                    message = message
                };
            }
            catch
            {
                throw;
            }
        }

        public void saveSkill(SkillDtl skill)
        {
            try
            {
                samrat.SkillDtls.Add(skill);
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public TrainingDtl saveTraining(TrainingDtl trainingDtl)
        {
            try
            {
                TrainingDtl result;
                result = samrat.TrainingDtls.Add(trainingDtl);
                samrat.SaveChanges();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public PaymentDtl savePayment(PaymentDtl payment)
        {
            try
            {
                PaymentDtl result;
                result = samrat.PaymentDtls.Add(payment);
                samrat.SaveChanges();
                return result;
            }
            catch
            {
                throw;
            }
        }
     
        public UserDetails login(UserDtl user)
        {
            try
            {
                UserDtl authLogin;
                string message = null;

                authLogin = samrat.UserDtls.SingleOrDefault(x => x.email.ToLower() == user.email.ToLower());

                if (authLogin != null)
                {
                    var pass = EncodePasswordToBase64(user.password);
                    authLogin = samrat.UserDtls.SingleOrDefault(x => x.email.ToLower() == user.email.ToLower() && x.password == pass);
                    if (authLogin != null)
                    {
                        message = "Logged In Successfully";
                    }
                    else
                    {
                        message = "Invalid Password";
                    }
                }
                else
                {
                    message = "Email Not Registered";
                }
                return new UserDetails()
                {
                    message = message,
                    token = null,
                    userInfo = authLogin
                };
            }
            catch
            {
                throw;
            }
            
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        // Update Data 

        public void updatePaymentAndCommisionById(int id, PaymentDtl paymentDtl)
        {
            try
            {
                PaymentDtl user = samrat.PaymentDtls.Find(id);
                user.commision = paymentDtl.commision;
                user.trainerFees = paymentDtl.trainerFees;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateUserProfileById(int id, UserDtl userDtl)
        {
            try
            {
                UserDtl user = samrat.UserDtls.Find(id);

                user.userName = userDtl.userName.Trim().ToLower();
                user.email = userDtl.email.Trim().ToLower();
                user.firstName = userDtl.firstName.Trim();
                user.lastName = userDtl.lastName.Trim();
                user.contactNumber = userDtl.contactNumber;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void updateTrainerProfileById(int id, UserDtl userDtl)
        {
            try
            {
                UserDtl user = samrat.UserDtls.Find(id);
                
                user.userName = userDtl.userName.Trim().ToLower();
                user.email = userDtl.email.Trim().ToLower();
                user.firstName = userDtl.firstName.Trim();
                user.lastName = userDtl.lastName.Trim();
                user.contactNumber = userDtl.contactNumber;
                user.yearOfExperience = userDtl.yearOfExperience;
                user.linkedinUrl = userDtl.linkedinUrl;
                user.trainerTechnology = userDtl.trainerTechnology;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateTrainingAndPaymentStatusById(int id)
        {
            try
            {
                TrainingDtl user = samrat.TrainingDtls.Find(id);
                user.trainingPaymentStatus = true;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateTrainingStatusById(int id)
        {
            try
            {
                TrainingDtl user = samrat.TrainingDtls.Find(id);
                user.status = "current";
                user.progress = 0;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void updateTrainingProgressById(int id, int progressValue)
        {
            try
            {
                TrainingDtl user = samrat.TrainingDtls.Find(id);
                user.progress = progressValue;
                if (progressValue == 100)
                {
                    user.status = "completed";
                }
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        
        public void updateTrainingRatingById(int id, int rating)
        {
            try
            {
                TrainingDtl user = samrat.TrainingDtls.Find(id);
                user.rating = rating;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void blockById(int id)
        {
            try
            {
                UserDtl user = samrat.UserDtls.Find(id);
                user.active = false;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        
        public void unBlockById(int id)
        {
            try
            {
                UserDtl user = samrat.UserDtls.Find(id);
                user.active = true;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(user).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void acceptTrainingRequestById(int id)
        {
            try
            {
                TrainingDtl trainingDtl = samrat.TrainingDtls.Find(id);
                trainingDtl.accept = true;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(trainingDtl).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void rejectTrainingRequestById(int id)
        {
            try
            {
                TrainingDtl trainingDtl = samrat.TrainingDtls.Find(id);
                trainingDtl.rejectNotify = true;
                samrat.Configuration.ValidateOnSaveEnabled = false;
                samrat.Entry(trainingDtl).State = System.Data.Entity.EntityState.Modified;
                samrat.Configuration.ValidateOnSaveEnabled = true;
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        // Delete Data

        public void DeleteSkillById(int id)
        {
            try
            {
                samrat.SkillDtls.Remove(samrat.SkillDtls.Find(id));
                samrat.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

  }
}
