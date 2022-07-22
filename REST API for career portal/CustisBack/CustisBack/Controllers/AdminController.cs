using Microsoft.AspNetCore.Mvc;
using CustisBack.Models.Database;
using CustisBack.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustisBack.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        [HttpPost(Name = "AddQuestion")]
        public string AddQuestion(Faq faq)
        {
            try
            {
                using var db = new MyDbContext();
                db.Faqs.Add(new Faq()
                {
                    Id = Guid.NewGuid(),
                    Question = faq.Question,
                    Answer = faq.Answer,
                });
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPut(Name = "UpdateQuestion")]
        public string UpdateQuestion(Faq faq)
        {
            try
            {
                using var db = new MyDbContext();
                var faqToUpdate = db.Faqs.FirstOrDefault(x => x.Id == faq.Id);
                if (faqToUpdate != null)
                {
                    faqToUpdate.Question = faq.Question;
                    faqToUpdate.Answer = faq.Answer;
                    db.SaveChanges();
                    return "Success";
                }
                return "Not found";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete(Name = "RemoveQuestion")]
        public string RemoveQuestion(Guid faqId)
        {
            try
            {
                using var db = new MyDbContext();
                var dbProject = db.Faqs.First(x => x.Id == faqId);
                db.Attach(dbProject);
                db.Remove(dbProject);
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost(Name = "AddVacancy")]
        public string AddVacancy(VacancyDTO vacancy)
        {
            try
            {
                using var db = new MyDbContext();

                var foundDirection = db.Directions.FirstOrDefault(x => x.Name == vacancy.direction);
                if (foundDirection == null)
                {
                    foundDirection = new Direction()
                    {
                        Id = Guid.NewGuid(),
                        Name = vacancy.direction
                    };
                }
                
                var newVacancy = new Vacancy()
                {
                    Id = Guid.NewGuid(),
                    Name = vacancy.name,
                    Description = vacancy.description,
                    Experience = vacancy.experience,
                    Schedule = vacancy.schedule,
                    Show = vacancy.showVacancy,
                    Direction = foundDirection,
                    WorkingConditions = vacancy.workingConditions,
                    Geo = vacancy.geo
                };
                
                var stacks = db.Stacks.Where(x => vacancy.stackList.Select(y=>y.name).ToArray().Contains(x.Name)).ToList();
                foreach (var foundStack in stacks)
                {
                    db.ListStackVacancies.Add(new ListStackVacancy()
                    {
                        Id = Guid.NewGuid(),
                        StackId = foundStack.Id,
                        Vacancy = newVacancy,
                    });
                }
                foreach (var notFoundStack in vacancy.stackList.ExceptBy(stacks.Select(x=>x.Name), y=>y.name).ToArray())
                {
                    var newStack = new Stack()
                    {
                        Id = Guid.NewGuid(),
                        Name = notFoundStack.name,
                        Color = notFoundStack.color
                    };
                    db.ListStackVacancies.Add(new ListStackVacancy()
                    {
                        Id = Guid.NewGuid(),
                        Stack = newStack,
                        Vacancy = newVacancy
                    });
                }
                
                db.Vacancies.Add(newVacancy);
                db.SaveChanges();
                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "UpdateVacancy")]
        public string UpdateVacancy(VacancyDTO vacancy)
        {
            try
            {
                using var db = new MyDbContext();
                var dbVacancy = db.Vacancies
                    .Include(x => x.Direction)
                    .Include(x => x.ListStackVacancies).ThenInclude(x => x.Stack)
                    .FirstOrDefault(x=>x.Id == vacancy.id);
                if (dbVacancy == null) throw new Exception("Vacancy not found");

                dbVacancy.Name = vacancy.name;
                dbVacancy.Description = vacancy.description;
                dbVacancy.Experience = vacancy.experience;
                dbVacancy.Schedule = vacancy.schedule;
                dbVacancy.WorkingConditions = vacancy.workingConditions;
                dbVacancy.Show = vacancy.showVacancy;
                dbVacancy.Geo = vacancy.geo;
                if (dbVacancy.Direction?.Name != vacancy.direction)
                {
                    var foundDirection = db.Directions.FirstOrDefault(x => x.Name == vacancy.direction);
                    if (foundDirection == null) foundDirection = new Direction() { Name = vacancy.direction };
                    dbVacancy.Direction = foundDirection;
                }
                
                var stacks = db.Stacks.Where(x => vacancy.stackList.Select(x=>x.name).ToArray().Contains(x.Name)).ToList();
                foreach (var stack in vacancy.stackList)
                {
                    if (dbVacancy.ListStackVacancies.Any(x=>x.Stack.Name == stack.name)) continue;
                    var foundStack = stacks.FirstOrDefault(x => x.Name == stack.name);
                    if (foundStack != null)
                    {
                        db.ListStackVacancies.Add(new ListStackVacancy()
                        {
                            Id = Guid.NewGuid(),
                            StackId = foundStack.Id,
                            Vacancy = dbVacancy,
                        });
                    }
                    else
                    {
                        var newStack = new Stack()
                        {
                            Id = Guid.NewGuid(),
                            Name = stack.name,
                            Color = stack.color
                        };
                        db.ListStackVacancies.Add(new ListStackVacancy()
                        {
                            Id = Guid.NewGuid(),
                            Stack = newStack,
                            Vacancy = dbVacancy,
                        });
                    }
                }

                foreach (var notFoundListStack in dbVacancy.ListStackVacancies.Where(x=>!vacancy.stackList.Select(x=>x.name).ToArray().Contains(x.Stack.Name)))
                {
                    db.ListStackVacancies.Remove(notFoundListStack);
                }
                
                db.SaveChanges();
                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "RemoveVacancy")]
        public string RemoveVacancy(Guid vacancyId)
        {
            try
            {
                using var db = new MyDbContext();
                var listStackVacancies = db.ListStackVacancies.Where(x => x.VacancyId == vacancyId).ToList();
                foreach (var stackVacancies in listStackVacancies)
                {
                    db.ListStackVacancies.Remove(stackVacancies);
                }
                var dbVacancy = db.Vacancies.First(x => x.Id == vacancyId);
                db.Attach(dbVacancy);
                db.Remove(dbVacancy);
                db.SaveChanges();
                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost(Name = "AddProject")]
        public string AddProject(ProjectDTO project)
        {
            try
            {
                using var db = new MyDbContext();
                db.Projects.Add(new Project()
                {
                    Id = Guid.NewGuid(),
                    Headline = project.headline,
                    Description = project.description,
                });
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "UpdateProject")]
        public string UpdateProject(ProjectDTO project)
        {
            try
            {
                using var db = new MyDbContext();
                var projectToUpdate = db.Projects.FirstOrDefault(x => x.Id == project.id);
                if (projectToUpdate != null)
                {
                    projectToUpdate.Headline = project.headline;
                    projectToUpdate.Description = project.description;
                    db.SaveChanges();
                    return "Success";
                }
                return "Not found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "RemoveProject")]
        public string RemoveProject(Guid projectId)
        {
            try
            {
                using var db = new MyDbContext();
                var dbProject = db.Projects.First(x => x.Id == projectId);
                db.Attach(dbProject);
                db.Remove(dbProject);
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost(Name = "AddStack")]
        public string AddStack(StackDTO stack)
        {
            try
            {
                using var db = new MyDbContext();
                db.Stacks.Add(new Stack()
                {
                    Id = Guid.NewGuid(),
                    Name = stack.name,
                    Color = stack.color
                });
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "UpdateStack")]
        public string UpdateStack(StackDTO stack)
        {
            try
            {
                using var db = new MyDbContext();
                var stackToUpdate = db.Stacks.FirstOrDefault(x => x.Id == stack.id);
                if (stackToUpdate != null)
                {
                    stackToUpdate.Name = stack.name;
                    stackToUpdate.Color = stack.color;
                    db.SaveChanges();
                    return "Success";
                }
                return "Not found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "RemoveStack")]
        public string RemoveStack([FromForm] Guid stackId)
        {
            try
            {
                using var db = new MyDbContext();
                var listStackVacancies = db.ListStackVacancies.Where(x => x.StackId == stackId).ToList();
                foreach (var stackVacancies in listStackVacancies)
                {
                    db.Remove(stackVacancies);
                }
                var dbStack = db.Stacks.First(x => x.Id == stackId);
                db.Remove(dbStack);
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost(Name ="UpdateStaticText")]
        public string UpdateStaticText(Text text)
        {
            try
            {
                using var db = new MyDbContext();
                var sstaticTextToUpdate = db.Texts.FirstOrDefault(x => x.Id == text.Id);
                if (sstaticTextToUpdate != null)
                {
                    sstaticTextToUpdate.Page = text.Page;
                    sstaticTextToUpdate.Description = text.Description;
                    db.SaveChanges();
                    return "Success";
                }
                return "Not found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        [HttpPost(Name ="UploadImage")]
        public string UploadImage([FromForm] IFormFile? file)
        {
            try
            {
                if (file != null)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    using var db = new MyDbContext();
                    var dbImage = new Image()
                    {
                        Id = Guid.NewGuid(),
                        Image1 = fileBytes,
                        ShowGallery = false,
                        Desc = file.FileName,
                        ContentType = file.ContentType
                    };
                    db.Images.Add(dbImage);
                    db.SaveChanges();
                    return dbImage.Id.ToString();
                }
                else return "Invalid request";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost(Name = "RemoveImage")]
        public string RemoveImage(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                var dbImage = db.Images.First(x => x.Id == id);
                db.Remove(dbImage);
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPut(Name = "UpdateEmail")]
        public string UpdateEmail(Tech emailNew)
        {
            try
            {
                using var db = new MyDbContext();
                var emailToUpdate = db.Teches.FirstOrDefault(x => x.Key == emailNew.Key);
                if (emailToUpdate != null)
                {
                    emailToUpdate.Value = emailNew.Value;
                    db.SaveChanges();
                    return "Success";
                }
                return "Not found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
