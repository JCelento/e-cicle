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
require("./EmptyListWarning.scss");
var Button_1 = require("../../../Shared/Components/Button");
var EmptyListWarning = (function (_super) {
    __extends(EmptyListWarning, _super);
    function EmptyListWarning() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.paths = {
            createProjectUrl: '/Projects/Add',
            createProjectLogo: ''
        };
        return _this;
    }
    EmptyListWarning.prototype.render = function () {
        return (React.createElement("section", { className: 'EmptyListWarning' },
            React.createElement("img", { className: 'center-block', src: '/images/List.png' }),
            React.createElement("section", { className: 'EmptyListWarning-textContainer' },
                React.createElement("p", { className: 'text-center' }, "It looks like you don't have any projects created."),
                React.createElement("p", { className: 'text-center' },
                    React.createElement(Button_1["default"], { link: true, href: this.paths.createProjectUrl, extraClassNames: ['btn-success'] }, "Create first project")))));
    };
    return EmptyListWarning;
}(React.Component));
exports["default"] = EmptyListWarning;
