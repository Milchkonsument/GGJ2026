using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatPanel : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TMPro.TextMeshProUGUI damageText;
    [SerializeField] private Slider healthSlider;

    public EnemyController Enemy;
    public CharacterController Character;

    public void LoadUI()
    {
        if (Character != null)
        {
            image.sprite = Character.Data.Sprite;
            nameText.text = Character.Data.Name;
            healthSlider.fillRect.GetComponent<Image>().color = Color.skyBlue;
            healthSlider.maxValue = Character.GetCurrentMaxMotivation();
            healthSlider.minValue = 0;
            healthSlider.value = Character.CurrentMotivation;
        }
        else if (Enemy != null)
        {
            image.sprite = Enemy.Data.Sprite;
            nameText.text = Enemy.Data.Name;
            healthSlider.fillRect.GetComponent<Image>().color = Color.softRed;
            healthSlider.maxValue = Enemy.GetCurrentMaxMotivation();
            healthSlider.minValue = 0;
            healthSlider.value = Enemy.CurrentMotivation;
        }
    }

    public void PlayHitAnimation()
    {
        StartCoroutine(PlayHitAnimationCoroutine());
    }

    public void PlayDamageAnimation(AttackOutcome outcome)
    {
        StartCoroutine(PlayDamageAnimationCoroutine(outcome));
        healthSlider.value = Character != null ? Character.CurrentMotivation : Enemy.CurrentMotivation;
    }

    private IEnumerator PlayHitAnimationCoroutine()
    {
        if (Enemy != null)
        {
        transform.position += Vector3.forward * 0.1f;
        yield return new WaitForSeconds(0.1f);
        transform.position += Vector3.back * 0.1f;
        yield return new WaitForSeconds(0.1f);
        transform.position += Vector3.back * 0.1f;
        yield return new WaitForSeconds(0.1f);
        transform.position += Vector3.forward * 0.1f;
        }

        if (Character != null)
        {
        transform.position += Vector3.back * 0.1f;
        yield return new WaitForSeconds(0.1f);
        transform.position += Vector3.forward * 0.1f;
        yield return new WaitForSeconds(0.1f);
        transform.position += Vector3.forward * 0.1f;
        yield return new WaitForSeconds(0.1f);
        transform.position += Vector3.back * 0.1f;
        }
    }

    private IEnumerator PlayDamageAnimationCoroutine(AttackOutcome outcome)
    {
        damageText.text = "-" + outcome.Damage.ToString();
        damageText.color = Color.white;

        if (outcome.Events.Contains(AttackEvents.Critical))
        {
            damageText.text += " (CRIT!)";
            damageText.color = Color.orange;
        }

        if (outcome.Events.Contains(AttackEvents.Miss))
        {
            damageText.text = "Miss!";
            damageText.color = Color.gray;
        }

        damageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageText.gameObject.SetActive(false);

        if (hitEffect != null && outcome.Events.Contains(AttackEvents.Hit))
        {
            hitEffect.Play();
        }

    }
}