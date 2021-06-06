<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TaskManager._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            <h3 id="pageTitle">Welcome to Tasks Manager! <button type="button" class="btn btn-primary" onclick="createTask()">Add</button></h3>
            <div id="status"></div>
            <div id="taskForm">
                <div class="panel panel-default">
                    <div class="panel-heading"><span id="taskFormTitle"></span></div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="taskName">Name</label>
                            <input type="text" class="form-control" id="taskName" style="max-width: 100%;">
                            <span id="taskNameError" class="text-danger" style="visibility: hidden;">Required</span>
                        </div>
                        <div class="form-group">
                            <label for="taskDependency">Dependent on</label>
                            <select class="form-control" id="taskDependency" style="max-width: 100%;"></select>
                        </div>
                        <div class="form-group">
                            <label for="taskName">Date Due</label>
                            <input type="datetime-local" class="form-control" id="taskDateDue" style="max-width: 100%;">
                        </div>
                        <span class="pull-right">
                            <button type="button" class="btn btn-default" onclick="cancelTask()">Cancel</button>
                            <button type="button" class="btn btn-primary" onclick="saveTask()">Save</button>
                        </span>
                        <div style="clear:both;"></div>
                    </div>
                </div>                
            </div>
            <div id="tasksList" style="padding-top: 10px;"></div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/task-manager-app.js"></script>
</asp:Content>