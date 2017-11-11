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
require("./ProjectsList.scss");
require("../../../Shared/Styles/helpers.scss");
var ProjectsList = (function (_super) {
    __extends(ProjectsList, _super);
    function ProjectsList() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ProjectsList.prototype.render = function () {
        return (React.createElement("section", { className: 'ProjectsList row' },
            React.createElement("div", { className: 'col-md-8 col-md-push-2' })));
    };
    return ProjectsList;
}(React.Component));
exports["default"] = ProjectsList;
