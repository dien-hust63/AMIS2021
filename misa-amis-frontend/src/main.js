import Vue from 'vue'
import App from './App.vue'
import router from './router/router.js';
import ResourcesVN from './js/resources/resourcevn';
import 'v2-datepicker/lib/index.css'; 
import V2Datepicker from 'v2-datepicker';

Vue.use(V2Datepicker);
Vue.config.productionTip = false

// ResoucesVN
Vue.prototype.$resourcesVN = ResourcesVN;

new Vue({
  render: h => h(App),
  router,
}).$mount('#app')
