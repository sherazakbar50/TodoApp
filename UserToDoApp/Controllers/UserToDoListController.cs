using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserToDoApp.ViewModels;

namespace UserToDoApp.Controllers
{
    [Authorize]
    public class UserToDoListController : Controller
    {
        // GET: UserToDoList
        public ActionResult Index()
        {
            return View();
        }

        #region ToDo List 
        /// <summary>
        /// Add new ToDo in List
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToDoInList(UserToDoListVm vm)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                using (UserToDoAppDBEntities et = new UserToDoAppDBEntities())
                {
                    var model = new UserToDoList
                    {
                        utd_order = vm.utd_order,
                        utd_title = vm.utd_title,
                        utd_priority = vm.utd_priority,
                        utd_date = vm.utd_date,
                        utd_created_date = System.DateTime.Now,
                        utd_created_by = userId
                    };
                    et.UserToDoLists.Add(model);
                    et.SaveChanges();
                }
                return Json(new { key = true, message = "Success", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { key = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Get ToDo List Against Current User
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserToDoList()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                using (UserToDoAppDBEntities et = new UserToDoAppDBEntities())
                {
                    var data = et.UserToDoLists
                   .Where(x => x.utd_created_by == userId)
                                .Select(x => new UserToDoListVm
                                {
                                    utd_id = x.utd_id,
                                    utd_order = x.utd_order,
                                    utd_title = x.utd_title,
                                    utd_priority = x.utd_priority,
                                    utd_date = x.utd_date
                                }).OrderBy(x => x.utd_order).ToList()
                                .Select(x => new UserToDoListVm
                                {
                                    utd_id = x.utd_id,
                                    utd_order = x.utd_order,
                                    utd_title = x.utd_title,
                                    utd_priority = x.utd_priority,
                                    date = (x.utd_date.HasValue) ? x.utd_date.Value.ToString("yyyy-MM-dd") : null
                                });
                    return Json(new { key = true, message = "Success", data }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { key = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// update rows order while clicking on arrow up
        /// </summary>
        /// <param name="curId"></param>
        /// <param name="curOrder"></param>
        /// <param name="prevId"></param>
        /// <param name="prevOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRowsOrderOnUpKey(int curId, int curOrder, int prevId, int prevOrder)
        {
            try
            {
                using (UserToDoAppDBEntities et = new UserToDoAppDBEntities())
                {
                    var curData = et.UserToDoLists.Where(x => x.utd_id == curId);
                    if (curData != null)
                    {
                        var data = curData.FirstOrDefault();
                        curOrder = curOrder - 1;
                        data.utd_order = curOrder;
                        et.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        et.SaveChanges();
                    }
                    var prevData = et.UserToDoLists.Where(x => x.utd_id == prevId);
                    if (prevData != null)
                    {
                        var data = prevData.FirstOrDefault();
                        prevOrder = prevOrder + 1;
                        data.utd_order = prevOrder;
                        et.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        et.SaveChanges();
                    }
                    return Json(new { key = true, message = "Success", }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return Json(new { key = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// update rows order while clicking on arrow down
        /// </summary>
        /// <param name="curId"></param>
        /// <param name="curOrder"></param>
        /// <param name="prevId"></param>
        /// <param name="prevOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRowsOrderOnDownKey(int curId, int curOrder, int nextId, int nextOrder)
        {
            try
            {

                using (UserToDoAppDBEntities et = new UserToDoAppDBEntities())
                {
                    var curData = et.UserToDoLists.Where(x => x.utd_id == curId);
                    if (curData != null)
                    {
                        var data = curData.FirstOrDefault();
                        curOrder = curOrder + 1;
                        data.utd_order = curOrder;
                        et.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        et.SaveChanges();
                    }
                    var prevData = et.UserToDoLists.Where(x => x.utd_id == nextId);
                    if (prevData != null)
                    {
                        var data = prevData.FirstOrDefault();
                        nextOrder = nextOrder - 1;
                        data.utd_order = nextOrder;
                        et.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        et.SaveChanges();
                    }
                    return Json(new { key = true, message = "Success", }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return Json(new { key = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Stop current execution
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteToDoFromList(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                using (UserToDoAppDBEntities et = new UserToDoAppDBEntities())
                {
                    var data = et.UserToDoLists.FirstOrDefault(c => c.utd_id == id);
                    if (data != null)
                    {
                        et.UserToDoLists.Remove(data);
                        et.SaveChanges();
                    }
                    var userToDos = et.UserToDoLists.Where(x => x.utd_created_by == userId);
                    if (userToDos != null)
                    {
                        var userToDoIds = userToDos.OrderBy(x => x.utd_order).Select(x => x.utd_id);
                        var order = 1;
                        foreach (var item in userToDoIds)
                        {
                            var toDo = et.UserToDoLists.Where(x => x.utd_id == item).FirstOrDefault();
                            toDo.utd_order = order++;
                            et.Entry(toDo).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    et.SaveChanges();
                }
                return Json(new { key = true, message = "Success", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { key = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion ToDo List
    }

}