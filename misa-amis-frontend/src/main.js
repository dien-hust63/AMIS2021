import Vue from 'vue'
import App from './App.vue'
import router from './router/router.js';
import 'v2-datepicker/lib/index.css';   // v2 need to improt css
import V2Datepicker from 'v2-datepicker';
Vue.use(V2Datepicker);
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
  router,
}).$mount('#app')
