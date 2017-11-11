"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
exports.__esModule = true;
var React = require("react");
var moment = require("moment");
require("./ProjectsListItem.scss");
var ProjectsListItem = (function (_super) {
    __extends(ProjectsListItem, _super);
    function ProjectsListItem() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ProjectsListItem.prototype.render = function () {
        return (React.createElement("a", { href: "/Projects/Details/" + this.props.project.Id },
            React.createElement("div", { className: 'ProjectsListItem row' },
                React.createElement("div", { className: 'col-md-12' },
                    React.createElement("div", { className: 'ProjectsListItem-summary row' },
                        React.createElement("div", { className: 'col-md-6 padding-none' },
                            React.createElement("span", { className: 'ProjectsListItem-name' }, this.props.project.Name)),
                        React.createElement("div", { className: 'col-md-6 padding-none' },
                            React.createElement("span", { className: 'pull-right' },
                                React.createElement("span", { className: 'glyphicon glyphicon-calendar' }),
                                " ",
                                moment(this.props.project.CreationDate).format("DD-MM-YYYY")))),
                    React.createElement("div", { className: 'row' },
                        React.createElement("div", { className: 'col-md-12 padding-none' },
                            React.createElement("span", { className: 'ProjectsListItem-description' }, this.props.project.Description)))))));
    };
    return ProjectsListItem;
}(React.Component));
exports["default"] = ProjectsListItem;
