<template>
  <div class="context-menu">
    <div class="context-menu__text">Sửa</div>
    <div class="context-menu-dropdown" :class="{'dropdown--show':isShow}">
      <div class="mi mi-16 mi-arrow-down--blue dropdown__icon" @click="showDropdown"></div>
      <div class="dropdown__content">
        <ul>
          <li>Nhân bản</li>
          <li>Xóa</li>
          <li>Ngừng sử dụng</li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
export default {
    name: "ContextMenu",
    data(){
        return {
            isShow: false,
        }
    },
    methods:{
        showDropdown(){
            this.isShow = !this.isShow
        }
    }
};
</script>
<style scoped>
@import url("../../../css/views/contextmenu/contextmenu.css");
</style>