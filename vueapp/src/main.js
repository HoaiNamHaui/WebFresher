import { createApp } from 'vue'
import App from './App.vue'
import {createRouter,createWebHistory} from "vue-router"
import EmployeeList from "./components/View/EmployeeList.vue";
import CashView from "./components/View/CashView.vue";
import DepositView from "./components/View/DepositView.vue";
import PurchaseView from "./components/View/PurchaseView.vue";
import SellView from "./components/View/SellView.vue";
import BillView from "./components/View/BillView.vue";
import RepositoryView from "./components/View/RepositoryView.vue";
import ToolView from "./components/View/ToolView.vue";
import AssetsView from "./components/View/AssetsView.vue";
import TaxView from "./components/View/TaxView.vue";
import PriceView from "./components/View/PriceView.vue";
import SyntheticView from "./components/View/SyntheticView.vue";
import BudgetView from "./components/View/BudgetView.vue";
import ReportView from "./components/View/ReportView.vue";
import AnalysisView from "./components/View/AnalysisView.vue";


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
createApp(App).use(router).mount('#app')
