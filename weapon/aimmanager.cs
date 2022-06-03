using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;
namespace metroidvania {
    public class aimmanager : ability
    {
        public Solver2D aiminggun;
        public Solver2D aiminglefthand;
        public Solver2D notaiminggun;
        public Solver2D notaiminglefthand;
        public Transform wheretoaim;
        public Transform wheretoplacehand;
        public Transform origin;
        public Bounds bounds;
        [SerializeField] private float autotargetradius; // use  to  close target auto aim


        private bool lockedon;  // use  to  close target auto aim

        [HideInInspector]
        public bool aiming;     //  use  to  close target auto aim

        protected override void initialization()
        {
            base.initialization();
            aiminggun.enabled = false;
            aiminglefthand.enabled = false;
            bounds.center = origin.position;
        }
        protected virtual void FixedUpdate()
        {
            Aiming();
            changearms();
            directionnalaim();
            checkedfortarget();
            bounds.center = origin.position;
        }
        protected virtual void Aiming()
        {
            if (input.Aimingheld() || directionnalaim())
            {
                checkedfortarget();
                if (!lockedon && !directionnalaim())
                {
                    notaiming();
                    return;
                }
                changearms();
                aiming = true;
                return;
            }
            notaiming();

        }
        // checking for target with overlap circle
    protected virtual   void checkedfortarget()
        {
            GameObject[] target;                //target game variable
            Collider2D[] collider = Physics2D.OverlapCircleAll(Weapon.gunbarrel.position, autotargetradius); //collider overlap
            if (collider.Length > 0)   
            {
                target = new GameObject[collider.Length];           //to create gameobject for target[] var with collidee.lengt[]
                for(int i = 0; i < collider.Length; i++)
                {
                    target[i] = collider[i].gameObject;             //make taget tp fet collide circle so we collder .gameobject
                   

                }

                lockedontarget(target);
            }

        }
        // this method used for not aiming
        void notaiming()
        {

            lockedon = false;
            aiming = false;
            changearms();
        }
        // this method used get lock enemy int collider circle 
     protected virtual GameObject lockedontarget(GameObject[] target)
        {


            Transform closertarget = null;
            float closedistance = Mathf.Infinity;
            Vector3 currentposition = transform.position;
            foreach (GameObject potentialtarget in target)
            {

                if (potentialtarget.tag == "target")
                {
                    Vector3 directionaltarget = potentialtarget.transform.position - currentposition;
                    float dsqrtotarget = directionaltarget.sqrMagnitude;
                    if (dsqrtotarget < closedistance)
                    {
                        closedistance = dsqrtotarget;
                        closertarget = potentialtarget.transform;
                    }
                }
            }
                if (closertarget != null)
                {
                    lockedon = true;
                    wheretoaim.transform.position = closertarget.position;
                    aiminggun.transform.GetChild(0).position = wheretoaim.transform.position;
                    
                     return closertarget.gameObject;
                   
                }
                lockedon = false;
                return null;

            


        }
        public virtual bool directionnalaim() // directional aim for up  down cross
        {

            if (character.isonladder)
            {
                return false;
            }
            if (input.Upheldweapon()&&!character.grabbingLedge)
            {
                wheretoaim.transform.position = new Vector2(bounds.center.x, bounds.max.y);
                return true;
            }
            if (input.Downheldweapon()&&!character.grabbingLedge)
            {
                wheretoaim.transform.position = new Vector2(bounds.center.x, bounds.min.y);
                return true;
            }
            if (input.Tiltedupheld())
            {
                if (!character.isfaceleft)
                {
                    wheretoaim.transform.position = new Vector2(bounds.max.x, bounds.max.y);
                    return true;
                }
                else
                {
                    wheretoaim.transform.position = new Vector2(bounds.min.x, bounds.max.y);
                    return true;
                }
            }
            if (input.Tilteddownheld())
            {
                if (!character.isfaceleft)
                {
                    wheretoaim.transform.position = new Vector2(bounds.max.x, bounds.min.y);
                    return true;
                }
                else
                {
                    wheretoaim.transform.position = new Vector2(bounds.min.x, bounds.min.y);
                    return true;
                }
            }
            return false;
        }
        public virtual void changearms()
        {
           
            if (Weapon.currenttimetillchangearms > 0||aiming)
            {
                notaiminggun.enabled = false;
                notaiminglefthand.enabled = false;
                aiminggun.enabled = true;
                aiminglefthand.enabled = true;
            }
            if (Weapon.currenttimetillchangearms <0&&!aiming)
            {
                notaiminggun.enabled = true;
                notaiminglefthand.enabled = true;
                aiminggun.enabled = false;
                aiminglefthand.enabled = false;
            }

        }
       
        private void OnDrawGizmos()
        {
            weapon weapons = GetComponent<weapon>();
            Gizmos.DrawWireCube(origin.position, bounds.size);
            Gizmos.DrawWireSphere(weapons.gunbarrel.position, autotargetradius);
        }


    }
}
