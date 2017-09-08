var ToDoList = function () {
    //Global variables
    var debuggingEnabled = false;
    //end Global variables

    //User ToDo List

    //Adding new ToDo in List
    var handleAddToList = function () {
        debugger;
        var todoTitle = $('#txt_todo_title').val();
        var todoPriority = $('#todo_priority_list').find('option:selected').text();
        var todoDate = $('#todo_date').val();
        var order = 1;
        if ($('#toDoList li').length > 0) {
            var lbl_order = $('#toDoList li:last > div').find('#lbl_order').text();
            order = parseInt(lbl_order) + 1;
        }
        var todoListVm = {
            utd_order: order,
            utd_title: todoTitle,
            utd_priority: todoPriority,
            utd_date: todoDate
        };

        $.ajax({
            url: '/UserToDoList/AddToDoInList',
            type: "POST",
            data: { vm: todoListVm },
            dataType: 'Json',
            success: function (response) {
                debugger;
                if (response.key == false) {
                    alert('Error');
                }
                else {
                    handleGetUserToDoList();
                }
            },
            complete: function () {
            },
            faiure: function () {
            }
        });
        $('#txt_todo_title').val('');
        $('#todo_priority_list').val('-1');
        $('#todo_date').val('');
    };

    //Getting user ToDo List
    var handleGetUserToDoList = function () {
        $.ajax({
            url: '/UserToDoList/GetUserToDoList',
            type: "Get",
            dataType: 'Json',
            success: function (response) {
                debugger;
                if (response.key == false) {
                    alert('Error');
                }
                else {
                    $('#toDoList').empty();
                    $.each(response.data, function (key, value) {
                        $('#toDoList').append('<li value=' + value.utd_id + '>'
                       + '<div class="form-group col-md-12"> '
                        + '<div class="col-md-1"><label class="lbl_order pull-left">' + value.utd_order + '</label></div> '
                           + '<div class="col-md-3"><label class="lbl_title pull-left">' + value.utd_title + '</label></div> '
                           + '<div class="col-md-3"><label class="pull-right">' + value.date + '</label></div> '
                             + '<div class="col-md-3"><span class="badge pull-right">' + value.utd_priority + '</span></div> '
                            + '<div class="col-md-2">'
                          + '  <span class="pull-right"> '
                             + ' <i class="fa fa-angle-double-down fa-6 move_down" aria-hidden="true" style="font-size: 18px;"></i>&nbsp;&nbsp; '
                              + '<i class="fa fa-angle-double-up fa-6 move_up" aria-hidden="true" style="font-size: 18px;"></i>&nbsp;&nbsp;'
                              + '<i onclick="ToDoList.removefromlist(this);" class="fa fa-remove fa-6" aria-hidden="true" style="font-size: 18px;"></i> '
                          + '  </span>'
                            + '</div> '
                         + '</div> '
                    + '</li>');
                    });
                }
            },
            complete: function () {
            },
            faiure: function () {
            }
        });
    };

    //Re ordering ToDos
    var handleToDoListItemUpAndDown = function () {
        //Moving ToDo up while clicking on up arrow
        $(document).on('click', '.move_up', function () {
            var curliOrder = $(this).closest('li').find('.lbl_order').text();
            var curliId = $(this).closest('li').attr('value');
            var prevliOrder = $(this).closest('li').prev().find('.lbl_order').text();
            var prevliId = $(this).closest('li').prev().attr('value');
            $.ajax({
                url: '/UserToDoList/UpdateRowsOrderOnUpKey',
                type: "POST",
                data: { curId: curliId, curOrder: curliOrder, prevId: prevliId, prevOrder: prevliOrder },
                dataType: 'Json',
                success: function (response) {
                    debugger;
                    if (response.key == false) {
                        alert('Error');
                    }
                    else {
                        handleGetUserToDoList();
                    }
                },
                complete: function () {
                },
                faiure: function () {
                }
            });
        });
        //Moving ToDo down while clicking on down arrow
        $(document).on('click', '.move_down', function () {
            var curliOrder = $(this).closest('li').find('.lbl_order').text();
            var curliId = $(this).closest('li').attr('value');
            var nextliOrder = $(this).closest('li').next().find('.lbl_order').text();
            var nextliId = $(this).closest('li').next().attr('value');
            $.ajax({
                url: '/UserToDoList/UpdateRowsOrderOnDownKey',
                type: "POST",
                data: { curId: curliId, curOrder: curliOrder, nextId: nextliId, nextOrder: nextliOrder },
                dataType: 'Json',
                success: function (response) {
                    if (response.key == false) {
                        alert('Error');
                    }
                    else {
                        handleGetUserToDoList();
                    }
                },
                complete: function () {
                },
                faiure: function () {
                }
            });
        });
    };

    //Deleting ToDo from List
    var handleRemoveFromList = function (el) {
        debugger;
        var id = $(el).closest('li').attr('value');
        $.ajax({
            url: '/UserToDoList/DeleteToDoFromList',
            type: "POST",
            data: { id: id },
            dataType: 'Json',
            success: function (response) {
                debugger;
                if (response.key == false) {
                    alert('Error');
                }
                else {
                    handleGetUserToDoList();
                }
            },
            complete: function () {
            },
            faiure: function () {
            }
        });
    };

    var handleOnClickEvents = function () {
        $('#todo_date').datepicker().on('changeDate', function (ev) {
            $('#todo_date').datepicker('hide');
        });

        $(document).on('click', 'li', function () {
            $(this).find('#lbl_title').toggleClass('strike');//.fadeOut('slow');
        });
    };

    return {
        init: function () {
            handleToDoListItemUpAndDown();
            handleGetUserToDoList();
            handleOnClickEvents();
            //Initialize Datepicker
            $("#todo_date").datepicker({
                dateFormat: "mm/dd/yy",
                showOtherMonths: true,
                selectOtherMonths: true,
                autoclose: true,
                changeMonth: true,
                changeYear: true,
                widgetPositioning: {
                    horizontal: 'auto',
                    vertical: 'bottom'
                }
            });
        },
        addToList: function () {
            handleAddToList();
        },
        removeFromList: function (el) {
            handleRemoveFromList(el);
        },
        success: function (data, status, xhr, modalId) {

        },
        error: function (xhr, status, error) {
        },
        log: function (msg) {
            if (debuggingEnabled == true) {
                console.log(msg);
            }
        }
    };
}();

jQuery(document).ready(function () {
    ToDoList.init();
});