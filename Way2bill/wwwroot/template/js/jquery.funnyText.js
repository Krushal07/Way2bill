!function(t){function o(){var t=(document.body||document.documentElement).style,o="transition";if("string"==typeof t[o])return!0;v="Moz Webkit Khtml O ms Icab".split(" ");for(var o=o.charAt(0).toUpperCase()+o.substr(1),e=0;e<v.length;e++)if("string"==typeof t[v[e]+o])return!0;return!1}t.fn.funnyText=function(e){function s(t,o){return t+Math.floor(Math.random()*(o+1))}function a(t,o){var a;return s(0,100)%2&&"both"==e.direction||"horizontal"==e.direction?"right"==t&&"top"==o?a="left top moveLeft":"right"==t&&"bottom"==o?a="left bottom moveLeft":"left"==t&&"top"==o?a="right top moveRight":"left"==t&&"bottom"==o&&(a="right bottom moveRight"):"right"==t&&"top"==o?a="right bottom moveDown":"right"==t&&"bottom"==o?a="right top moveUp":"left"==t&&"top"==o?a="left bottom moveDown":"left"==t&&"bottom"==o&&(a="left top moveUp"),a}function i(t,o){setTimeout(function(){n(t)},o*e.speed)}function n(t){var e=t.height()/2,s=t.width()/2,a=t.find(".active");o()?a.hasClass("moveRight")?t.hasClass("moved")?t.css("left","0px"):t.css("left","-"+s+"px"):a.hasClass("moveLeft")?t.hasClass("moved")?t.css("left","-"+s+"px"):t.css("left","0px"):a.hasClass("moveUp")?t.hasClass("moved")?t.css("top","-"+e+"px"):t.css("top","0px"):a.hasClass("moveDown")&&(t.hasClass("moved")?t.css("bottom","0px"):t.css("bottom",e+"px")):a.hasClass("moveRight")?t.hasClass("moved")?t.animate({left:"0px"},400):t.animate({left:"-"+s+"px"},400):a.hasClass("moveLeft")?t.hasClass("moved")?t.animate({left:"-"+s+"px"},400):t.animate({left:"0px"},400):a.hasClass("moveUp")?t.hasClass("moved")?t.animate({top:"-"+e+"px"},400):t.animate({top:"0px"},400):a.hasClass("moveDown")&&(t.hasClass("moved")?t.animate({bottom:"0px"},400):t.animate({bottom:e+"px"},400)),t.toggleClass("moved")}e=t.extend({speed:700,borderColor:"black",activeColor:"white",color:"black",direction:"both"},e);var r=t(this);r.addClass("funnyText");var h,l,p,m=t(this),c=t.trim(m.text()).split(""),f=["top","bottom"],v=["left","right"];m.html(""),h=t("<style>"+r.selector+".funnyText span.active { color: "+e.activeColor+"; text-shadow: -1px 0 "+e.borderColor+", 0 1px "+e.borderColor+", 1px 0 "+e.borderColor+", 0 -1px "+e.borderColor+";} "+r.selector+".funnyText span{color: "+e.color+"; font-size:"+e.fontSize+";}</style>"),t("html > head").append(h);for(var d=0;d<c.length;d++){l=f[s(0,100)%2],h=v[s(0,100)%2]," "==c[d]&&(c[d]="&nbsp;");var x='<span class="normal  '+l+" "+h+'">'+c[d]+"</span>";do activePositionXY=a(h,l);while(activePositionXY==p&&"both"==e.direction);p=activePositionXY,m.append('<div class="charWrap">'+x+('<span class="active '+activePositionXY+'">'+c[d]+"</span>")+"</div>")}r.find(".charWrap").each(function(){var o=t(this).find("span").width(),e=t(this).find("span").height();t(this).css({width:2*o+"px",height:2*e+"px"});var s=t(this);s.find(".normal").hasClass("bottom")&&s.css("top","-"+s.find(".normal").height()+"px"),s.find(".normal").hasClass("right")&&s.css("left","-"+s.find(".normal").width()+"px"),t(this).wrap('<div class="character" style="width:'+o+"px; height: "+e+'px"></div>')}),setInterval(function(){var t=s(2,6);do var o=s(0,c.length-1);while(""===o);i(r.find(".charWrap").eq(o),t)},1*e.speed),t(".charWrap").hover(function(){t(this).hasClass("moved")||n(t(this))})}}(jQuery);