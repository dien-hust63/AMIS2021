<template>
  <div class="context-menu" :style="positions">
    <ul class="dropdown__content">
      <li v-for="(item, index) in contextContent" :key="index" @click="item.callbackFunc">
        {{ item.text }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "ContextMenu",
  props: {
    contextContent: {
      type: Array,
      default: () => {
        return [{ text: "default", callbackFunc: () => {} }];
      },
    },
    contextPosition:{
      type: Object,
      default: ()=>{
        return {
          top: 0,
          bottom: 0
        }
      }
    }
  },
  data() {
    return {};
  },
  computed: {
    positions: function(){
      return{
        top: this.contextPosition.top + 'px',
        left: this.contextPosition.left + 'px',
      }
    }
  }
};
</script>
<style scoped>
@import url("../../../css/views/contextmenu/contextmenu.css");
</style>