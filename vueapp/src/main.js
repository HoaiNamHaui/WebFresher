import { vue3Debounce } from 'vue-debounce'
import { createApp } from 'vue'
import App from './App.vue'
import {createRouter,createWebHistory} from "vue-router"
import EmployeeList from "./components/Views/EmployeeList.vue";
import CashView from "./components/Views/CashView.vue";
import DepositView from "./components/Views/DepositView.vue";
import PurchaseView from "./components/Views/PurchaseView.vue";
import SellView from "./components/Views/SellView.vue";
import BillView from "./components/Views/BillView.vue";
import RepositoryView from "./components/Views/RepositoryView.vue";
import ToolView from "./components/Views/ToolView.vue";
import AssetsView from "./components/Views/AssetsView.vue";
import TaxView from "./components/Views/TaxView.vue";
import PriceView from "./components/Views/PriceView.vue";
import SyntheticView from "./components/Views/SyntheticView.vue";
import BudgetView from "./components/Views/BudgetView.vue";
import ReportView from "./components/Views/ReportView.vue";
import AnalysisView from "./components/Views/AnalysisView.vue";
import BaseButton from '../src/components/Base/button/BaseButton.vue'

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
]
const router = createRouter({
    history: createWebHistory(),
    routes: routers,
})
createApp(App).use(router).directive('debounce', vue3Debounce({ lock: true })).mount('#app')
const app = createApp({})
app.component('BaseButton', BaseButton)
