using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using DSLLanguage.ViewModels.Staff;
using DSLBuilderExpression;


namespace DSLLanguage.Controllers
{
    public class StaffController : Controller
    {
        [HttpGet]
        public IActionResult StaffList()
        {
            var staff = new FakeStaffListDB
            {
                Title = "Сотрудники",
                Staff = new List<FakeStaffListItemDB>
                {
                    new FakeStaffListItemDB
                    {
                        Id = 1,
                        StaffName = "Генеральный директор",
                        Competension = "Менеджмент, Управление персоналом",
                        Date = DateTimeOffset.Now,
                        FIO = "Петров Петр Алексеевич"
                    },
                    new FakeStaffListItemDB
                    {
                        Id = 2,
                        StaffName = "Специалист по подбору персонала",
                        Competension = "Общение, Коммуникация, Баловство",
                        Date = DateTimeOffset.Now,
                        FIO = "Сергеевна Алина Юрьевна"
                    },
                    new FakeStaffListItemDB
                    {
                        Id = 3,
                        StaffName = "Специалист по подбору персонала",
                        Competension = "Общение, Коммуникация, Баловство",
                        Date = DateTimeOffset.Now,
                        FIO = "Игнатьевна Алиса Новоселова"
                    },
                    new FakeStaffListItemDB
                    {
                        Id = 4,
                        StaffName = "Менджер по закупкам",
                        Competension = "Общение, Коммуникация, Баловство",
                        Date = DateTimeOffset.Now,
                        FIO = "Рогатых Игорь Олегович"
                    },
                    new FakeStaffListItemDB
                    {
                        Id = 5,
                        StaffName = "Кладовщик",
                        Competension = "Бережливость",
                        Date = DateTimeOffset.Now,
                        FIO = "Горынин Бодболт Хишитбат"
                    }
                }
            };

            var Builder = new ComponentList(
                staff.Staff.Select(
                    e => new Row(
                        new Column(
                            new Item(
                                title: e.StaffName,
                                text: e.FIO,
                                date: e.Date.ToString("dd mm yyy"),
                                smallText: e.Competension
                            )
                        )
                    )
                ).ToArray()
            );

            var modelView = new StaffListViewModel().Init(Builder.Generate());

            return View(nameof(StaffList), modelView);
        }


    }


    public class FakeStaffListDB
    {
        public string Title { get; set; }

        public List<FakeStaffListItemDB> Staff { get; set; }
    }

    public class FakeStaffListItemDB
    {
        public int Id { get; set; }

        public string StaffName { get; set; }

        public DateTimeOffset Date { get; set; }

        public string FIO { get; set; }

        public string Competension { get; set; }
    }
}
