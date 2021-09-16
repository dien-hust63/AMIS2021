import Vue from 'vue'
import VueRouter from 'vue-router'
import EmployeeContent from "../components/views/employees/EmployeeContent.vue";

Vue.use(VueRouter)

const routes = [
    {path: "/", name:"EmployeeContent", component: EmployeeContent},
    
]

const router = new VueRouter({
    mode: 'history',
    routes : routes 
})

export default router;