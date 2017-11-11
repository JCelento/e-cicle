"use strict";
exports.__esModule = true;
var ReactDOM = require("react-dom");
var React = require("react");
require("../Shared/Styles/helpers.scss");
var ProjectsListContainer_1 = require("./Components/ProjectsListContainer/ProjectsListContainer");
ReactDOM.render(React.createElement(ProjectsListContainer_1["default"], null), document.getElementById('react-root'));
if (module.hot) {
    module.hot.accept();
}
