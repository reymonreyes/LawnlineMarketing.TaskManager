let tempTaskId = null;
let taskList = null;

function initPage() {
    $('#taskForm').css('display', 'none');
}

function resetTaskList() {
    $('#tasksList').empty();
    let list = '<ul class="task-list list-group">';
    taskList.forEach(task => {
        console.log(task);
        let dateDue = new Date(task.DateDue);
        list += '<li class="list-group-item"><button type="button" title="edit" class="btn btn-default btn-sm" onclick="editTask(\'' + task.Id +
            '\')"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button><label>' + task.Name + '<input type="checkbox" ' + (task.Complete ? 'checked' : '') +
            ' onchange="taskCompleted(\'' + task.Id + '\')" /><span class="pull-right" style="margin-right: 10px;">' + dateDue.toLocaleString() + '</span></label></li>';
    });
    list += '</ul>';
    $('#tasksList').append(list);
}
function resetTaskDependencyControl() {
    $('#taskDependency').empty();
    $('#taskDependency').val('');
    $('#taskDependency').append('<option value=""></option>');
    taskList.forEach(task => {
        $('#taskDependency').append('<option value="' + task.Id + '">' + task.Name + '</option>');
    });
}
function loadTasks() {
    $('#status').text('loading...');
    $.getJSON('api/tasks', function (tasks) {
        console.log(tasks);
        taskList = tasks;               
        resetTaskList();
        resetTaskDependencyControl();
        $('#status').text('');
    });
}

function createTask() {
    $('#status').text('');
    $('#taskForm').css('display', 'inherit');
    $('#taskName').val('');
    $('#taskFormTitle').text('Add Task');
    $('#taskDependency').val('');
    $('#taskDateDue').val('');
}

function saveTask() {
    //validate
    let taskName = $('#taskName').val();
    taskName = $.trim(taskName);
    if (taskName.length < 1) {
        $('#taskNameError').css('visibility', 'visible');
        return;
    }
    //proceed
    let taskDateDue = $('#taskDateDue').val();
    //taskDateDue = (new Date(taskDateDue)).toJSON();
    let taskDependencyId = $('#taskDependency').val();
    $('#taskNameError').css('visibility', 'hidden');
    $('#status').text('Saving task...');
    let task = { Name: taskName, DependentTaskId: taskDependencyId, DateDue: taskDateDue };
    if (tempTaskId !== null) {
        task.Id = tempTaskId;
        let putRequest = $.ajax({ method: 'PUT', url: 'api/tasks', contentType: 'application/json', data: JSON.stringify(task) });
        putRequest.then(function () {
            tempTaskId = null;
            cancelTask();
            loadTasks();
        }).catch(function (error) {
            $('#status').text('');
            alert('Unable to process request.');
        });
    }
    else {
        let postRequest = $.post('api/tasks', task);
        postRequest.then(function (result) {
            $('#taskForm').css('display', 'none');
            console.log('data saved successfully');
            $('#status').text('');
            tempTaskId = null;
            loadTasks();
        });
    }
}

function cancelTask() {
    createTask();
    $('#taskForm').css('display', 'none');
}

function editTask(taskId) {
    resetTaskDependencyControl();
    let optionSelected = $('option[value="' + taskId + '"');
    $(optionSelected[0]).remove();
    $('#status').text('loading...');
    $.getJSON('api/tasks/' + taskId).done(function (task) {
        console.log('gettask ok');
        if (task) {
            console.log(task);
            tempTaskId = task.Id;
            createTask();
            $('#taskName').val(task.Name);
            $('#taskFormTitle').text('Edit Task');
            if (task.DateDue) {
                let dateDue = moment(task.DateDue);
                //dateDue.form
                $('#taskDateDue').val(dateDue.format('YYYY-MM-DDTHH:mm'));
            }
            //$('#taskDateDue').val(task.DateDue);

        }

        $('#status').text('');
    }).fail(function (error) {
        console.log(error);
        $('#status').text('');
        alert('error!');
    });
}

function taskCompleted(taskId) {
    console.log(taskId);
    $('#status').text('processing...');
    let postRequest = $.post('api/tasks/' + taskId + '/completed', {});
    postRequest.then(function (result) {
        $('#status').text('');
        tempTaskId = null;
        loadTasks();
    });
}

initPage();
loadTasks();