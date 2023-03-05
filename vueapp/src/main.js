import { vue3Debounce } from 'vue-debounce'
import { createApp } from 'vue'
import App from './App.vue'
import {createRouter,createWebHistory} from "vue-router"
import EmployeeList from "./components/views/EmployeeList.vue";
import CashView from "./components/views/CashView.vue";
import DepositView from "./components/views/DepositView.vue";
import PurchaseView from "./components/views/PurchaseView.vue";
import SellView from "./components/views/SellView.vue";
import BillView from "./components/views/BillView.vue";
import RepositoryView from "./components/views/RepositoryView.vue";
import ToolView from "./components/views/ToolView.vue";
import AssetsView from "./components/views/AssetsView.vue";
import TaxView from "./components/views/TaxView.vue";
import PriceView from "./components/views/PriceView.vue";
import SyntheticView from "./components/views/SyntheticView.vue";
import BudgetView from "./components/views/BudgetView.vue";
import ReportView from "./components/views/ReportView.vue";
import AnalysisView from "./components/views/AnalysisView.vue";
import BaseButton from '../src/components/base/button/BaseButton.vue'
import CategoryView from './components/views/CategoryView.vue'
const routers = [
    {path:"/" , component: EmployeeList},
    {path:"/CashView" , component: CashView},
    {path:"/DepositView" , component: DepositView},
    {path:"/PurchaseView" , component: PurchaseView},
    {path:"/SellView" , component: SellView},
    {path:"/BillView" , component: BillView},
    {path:"/RepositoryView" , component: RepositoryView},
    {path:"/ToolView" , component: ToolView},
    {path:"/AssetsView" , component: AssetsView},
    {path:"/TaxView" , component: TaxView},
    {path:"/PriceView" , component: PriceView},
    {path:"/SyntheticView" , component: SyntheticView},
    {path:"/BudgetView" , component: BudgetView},
    {path:"/ReportView" , component: ReportView},
    {path:"/AnalysisView" , component: AnalysisView},
    {path:"/CategoryView" , component: CategoryView},
]
const router = createRouter({
    history: createWebHistory(),
    routes: routers,
})
createApp(App).use(router).directive('debounce', vue3Debounce({ lock: true })).mount('#app')
const app = createApp({})
app.component('BaseButton', BaseButton)
