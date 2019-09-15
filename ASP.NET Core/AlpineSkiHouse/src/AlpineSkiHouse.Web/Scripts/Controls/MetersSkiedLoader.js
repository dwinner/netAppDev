System.register(["./MetersSkied"], function (exports_1, context_1) {
    "use strict";
    var MetersSkied_1, container, graph;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (MetersSkied_1_1) {
                MetersSkied_1 = MetersSkied_1_1;
            }
        ],
        execute: function () {
            container = document.getElementById("metersSkiedChart");
            graph = new MetersSkied_1.MetersSkied(container);
            console.log("I made a graph");
        }
    };
});
